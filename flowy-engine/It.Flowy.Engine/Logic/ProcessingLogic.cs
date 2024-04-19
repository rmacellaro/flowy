using It.Flowy.Engine.Activities;
using It.Flowy.Engine.Helpers;
using It.Flowy.Engine.Models.Exceptions;
using It.Flowy.Engine.Models.Modelling;
using It.Flowy.Engine.Models.Processing;
using It.Flowy.Engine.Services.Modelling;
using It.Flowy.Engine.Services.Processing;
using log4net;
using Newtonsoft.Json.Linq;

namespace It.Flowy.Engine.Logic;

public interface IProcessingLogic {
  List<Instance>? GetInstancesByIdProcess(long idProcess);
  Instance? GetInstanceByIdWire(long idWire);
  Node? GetStartNodeByIdDistribution(long idDistribution);
  Instance? Start(JObject request);
  Instance? Continue(JObject request);
}

public class ProcessingLogic(
    IProcessesService procSrv,
    IDistributionsService relSrv,
    INodesService nodSrv,
    IInstancesService instSrv,
    IWiresService wirSrv,
    IActivitiesService actSrv
) : IProcessingLogic {

  private static readonly ILog log = LogManager.GetLogger(typeof(ProcessingLogic));

  private readonly IProcessesService ProcessesService = procSrv;
  private readonly IDistributionsService DistributionsService = relSrv;
  private readonly INodesService NodesService = nodSrv;
  private readonly IInstancesService InstancesService = instSrv;
  private readonly IWiresService WiresService = wirSrv;
  private readonly IActivitiesService ActivitiesService = actSrv;

  public List<Instance>? GetInstancesByIdProcess(long idProcess) {
    return InstancesService.GetInstancesByIdProcess(idProcess, ["Wires", "Wires.Node", "Datas"]);
  }

  public Instance? GetInstanceByIdWire(long idWire) {
    Instance? i = InstancesService.GetInstanceByIdWire(idWire, ["Wires", "Datas"]);
    if (i != null && i.Wires != null){
      foreach (Wire w in i.Wires){
        if (w.IdNode != null){
          w.Node = NodesService.GetNodeById(w.IdNode.Value, ["Datas", "Activities", "Activities.ActivityDefinition",  "Activities.Datas", "OutputLinks"]);
          if (w.Node == null) { throw new Exception("Node not found: " + w.IdNode); }
          if (w.Node.Datas == null || w.Node.Datas.Count <= 0) { throw new Exception("No Configurations to Node"); }
          // rimuovo dalla lista le configurazioni che sono destinate solo al motore di processo
          w.Node.Datas = w.Node.Datas.Where(
            c => c.Name != null && !c.Name.StartsWith("System.BE")
            //c => c.IsForProcessingOnly.HasValue && !c.IsForProcessingOnly.Value
          ).ToList();
        }
      }
    }
    return i;
  }

  public Node GetStartNodeByIdDistribution(long idDistribution){
    Node? startNode = NodesService.GetNodeByKeyAndIdDistribution("START", idDistribution, ["Datas", "Activities", "Activities.ActivityDefinition",  "Activities.Datas", "OutputLinks"]);
    // se non trovo il nodo di start sollevo eccezione
    if (startNode == null) { throw new Exception("No Start Node to idDistribution: " + idDistribution); }
    if (startNode.Datas == null || startNode.Datas.Count <= 0) { throw new Exception("No Datas to Start Node"); }

    // TODO: rimuovo dalla lista le configurazioni che sono destinate solo al motore di processo
    /*
    startNode.Configs = startNode.Configs.Where(
      c => c.Name != null && !c.Name.StartsWith("System.BE")
      //c => c.IsForProcessingOnly.HasValue && !c.IsForProcessingOnly.Value
    ).ToList();*/
    return startNode;
  }

  /*public Interaction? GetInteractionWithConfigurationsById(long idInteraction){
      Interaction? interaction = InteractionsService.GetInteractionById(idInteraction, true);
      if (interaction == null) { throw new Exception("No Interaction by id: " + idInteraction);}
      if (interaction.Configurations == null || interaction.Configurations.Count <= 0){ throw new Exception("No Configurations to Interaction");}
      // rimuovo dalla lista le configurazioni che sono destinate solo al motore di processo
      interaction.Configurations = interaction.Configurations.Where(
          c => c.IsForProcessingOnly.HasValue && !c.IsForProcessingOnly.Value
      ).ToList();
      return interaction;
  }*/

  public Instance? Start(JObject request){
    log.Debug("Start new Instance");
    try{
      long? idDistribution = request.GetValue("idDistribution")?.Value<long>();
      long? idProcess = request.GetValue("idProcess")?.Value<long>();
      long? idActivity = request.GetValue("idActivity")?.Value<long>();

      // if neither an idDistribution nor an idProcess was specified, I raise an exception
      if (!idDistribution.HasValue && !idProcess.HasValue) { throw new ProcessingException("PS.IDIP.NS", "idDistribution or idProcess not specified"); }

      // I need to retrieve the distribution to run
      Distribution? distribution = null;
      // if an idDistribution was specified in the input data then I retrieve the distribution
      if (idDistribution.HasValue){
        distribution = DistributionsService.GetDistributionById(idDistribution.Value, null);
        // if I don't find the distribution corresponding to the id specified in the input data in the database, then I raise an exception
        if (distribution == null) { throw new ProcessingException("PS.DWID.NF","Distribution not found with idDistribution: " + idDistribution.Value); }
      }
      else if (idProcess.HasValue){
        List<Distribution>? distributions = DistributionsService.GetDistributionsByIdProcess(idProcess.Value);
        if (distributions == null || distributions.Count <= 0) { throw new ProcessingException("PS.DWIP.NF","Distributions not found with idProcesso: " + idProcess.Value); }
        foreach (Distribution rel in distributions){
          if (!rel.IsEnabled) { continue; }
          if (rel.State == null) { continue; }
          if (rel.State.Equals("DRAFT")) { continue; }
          // se utente non abilitato al test sollevo eccezione
          //if (rel.State.Equals("TEST") )
          distribution = rel;
          break;
        }
        if (distribution == null) { throw new ProcessingException("PS.DAWIP.NF","Distribution active not found with idProcess: " + idProcess.Value); }
      }
      if (distribution == null || !distribution.Id.HasValue) { throw new ProcessingException("PS.DTR.NF", "Distribution to run not found"); }
      log.Debug("Retrived Distribution to run, with id: " + distribution.Id.Value);
      
      // TODO : if it is a test distribution and the user is not enabled to run it I raise an exception

      // Retrieve start node to run for distribution
      log.Debug("Retrieve start node to run for distribution");
      Node? start = NodesService.GetNodeByKeyAndIdDistribution("START", distribution.Id.Value, ["Datas", "Activities", "Activities.ActivityDefinition",  "Activities.Datas", "OutputLinks"]);
      if (start == null || !start.Id.HasValue) { throw new ProcessingException("PS.SNBID.NF","Node of Start not found by idDistribution: " + distribution.Id.Value); }
      if (start.Activities == null) { throw new ProcessingException("PS.NS.WAC","Node of Start without activities configured: " + start.Id);}

      // if you have not specified an idActivity to run for the node
      // and there is at least one manual activity, then I won't continue
      if (!idActivity.HasValue) {
        log.Debug("IdActivity not specified, checks if the node contains configured manual activities");
        if(start.Activities.Count(a => a.Datas.CheckValue(ConfigActivity.PROCESSING_ACTIVITY_ISAUTOMATIC, "False", true)) > 0) {
          throw new ProcessingException("PS.IATE.NS","IdActivity to execute not specified");
        } else {
          log.Debug("No manual activities configured for the current node: " + start.Id);
        }
      }

      // I can proceeding to create new instance
      log.Debug("Proceeding to create new instance");
      Instance newInstance = new(){
        CreatedDateTime = DateTime.Now,
        IdDistribution = distribution.Id,
        Distribution = distribution,
        Key = Guid.NewGuid().ToString()
      };
      InstancesService.Insert(newInstance);
      if (!newInstance.Id.HasValue) { throw new ProcessingException("PS.CNIOD.E","Error creating new instance on database");}
      log.Debug("New Instance created with id: " + newInstance.Id.Value);

      // I proceeding to create new wire for this the new instance
      log.Debug("Proceeding to create new wire, for this new instance");
      Wire wire = new(){
        IdInstance = newInstance.Id,
        Instance = newInstance,
        State = "STARTING",
        CreatedDateTime = DateTime.Now,
        UpdatedDateTime = DateTime.Now,
        IdNode = start.Id,
        Node = start
      };
      WiresService.Insert(wire);
      if (!wire.Id.HasValue) { throw new ProcessingException("PS.CNWOD.E","Error creating new wire on database for this instance: " + newInstance.Id.Value);}
      log.Debug("New Wire for this new instance, created with id: " + wire.Id.Value);

      // processing the new instance
      log.Debug("Processing the this new instance: " + newInstance.Id.Value);
      Processing(wire, request);
      return checkEndOfInstance(newInstance.Id.Value);    
    } catch(Exception ex){
      log.Error(ex);
      throw;
    }
  }

  public Instance? Continue(JObject request) {
    long? idWire = request.GetValue("idWire")?.Value<long>();
    long? idInstance = request.GetValue("idInstance")?.Value<long>();

    Wire? wire = null;
    if (idWire.HasValue){
      wire = WiresService.GetWireById(idWire.Value, true);
      if (wire == null) { throw new Exception("Wire not found by id : " + idWire.Value); }
    }
    else if (idInstance.HasValue){
      Instance? instance = InstancesService.GetInstanceById(idInstance.Value, ["Wires", "Datas"]);
      if (instance == null) { throw new Exception("Instance not found by id : " + idInstance.Value); }
      if (instance.Wires == null) { throw new Exception("Instance.Wires not found by idInstance: " + idInstance.Value); }
      if (instance.Wires.Count > 1) { throw new Exception("Multiple Wires in instance: " + idInstance.Value); }
      var first = instance.Wires.First();
      if (!first.Id.HasValue) { throw new Exception("Instance Wire without id"); }
      wire = WiresService.GetWireById(first.Id.Value, true);
    }
    else{
      throw new Exception("idWire or idInstance not found");
    }
    if (wire == null || wire.IdNode == null) { throw new Exception("Wire to processing not found"); }
    wire.Node = NodesService.GetNodeById(wire.IdNode.Value, ["Datas", "Activities", "Activities.ActivityDefinition", "Activities.Datas", "OutputLinks"]);

    // processo
    Processing(wire, request);

    if (wire.Instance == null || !wire.Instance.Id.HasValue){ throw new Exception("No Instance");}
    return checkEndOfInstance(wire.Instance.Id.Value);
  }


  private Instance? checkEndOfInstance(long idInstance) {
    Instance? instance = InstancesService.GetInstanceById(idInstance, [ "Wires", "Datas" ]);
    // se non trovo l'istanza qualcosa è andato storto
    if (instance == null) { throw new Exception("Instance not found: " + idInstance);}
    // se tutti i wire sono chiusi chiudo l'istanza

    return instance;
  }

  private void Processing(Wire wire, JObject data){

    if (wire == null) { throw new Exception("Wire not found in elaboration"); }
    if (wire.Instance == null) { throw new Exception("Wire without Instance: " + wire.Id); }
    if (wire.Instance.IdDistribution == null) { throw new Exception("wire.Instance.IdDistribution is null" + wire.Instance.Id); }
    if (wire.Node == null) { throw new Exception("Wire without Node : " + wire.Id); }
    if (wire.Node.Datas == null) { throw new Exception("Wire.Node Without Datas " + wire.Node.Id); }
    if (wire.Node.Activities == null) { throw new Exception("Wire.Node Without Activities " + wire.Node.Id); }
    if (wire.Node.OutputLinks == null) { throw new Exception("Wire.Node Without OutputLinks " + wire.Node.Id); }
    if (wire.State == "PROCESSING") { throw new Exception("Wire in processing");}

    try{
      wire.State = "PROCESSING";
      WiresService.Update(wire);

      // preparo la lista dei prossimi nodi da eseguire
      List<string> nexts = [];
      
      // recupero se specificato in id activity da eseguire
      long? idActivity = data.GetValue("idActivity")?.Value<long>();

      // ciclo su tutte le activity configurate per il nodo per vedere quale eseguire
      var activities = wire.Node.Activities.OrderBy(a => a.Index);
      foreach(Activity activity in activities) {
        // se l'activity non ha configurazioni vado avanti
        if (activity.Datas == null) { throw new Exception("Activity without configs: " + activity.Id);}
        if (activity.ActivityDefinition == null || activity.ActivityDefinition.HasFrontEnd == null) { continue; }

        // se l'activity è automatica (non ha interfaccia front-end) allora la eseguo
        bool isAutomatic = !activity.ActivityDefinition.HasFrontEnd.Value;
        bool isActivityToExecute = idActivity.HasValue && (activity.Id == idActivity.Value);

        // oppure è l'activity
        if (isAutomatic || isActivityToExecute) {
          // eseguo l'activity
          BaseActivity activityExecution = GetActivityByActivityDefinition(activity.ActivityDefinition);
          activityExecution.Setup(wire);
          var nexta = activityExecution.Execution();
          nexts.AddRange(nexta);
        }
      }

      /*// recupero l'activity da eseguire per il nodo
      long? idActivity = data.GetValue("idActivity")?.Value<long>();
      if (!idActivity.HasValue){ throw new Exception("IdActivity to run not found!");}
      Activity? activity = ActivitiesService.GetActivityById(idActivity.Value, ["Configs"]);
      if (activity == null) { throw new Exception("No Activity to run");}
      if (activity.Configs == null) { throw new Exception("Activity without configs: " + activity.Id);}

      string? activityName = activity.Configs.FirstOrDefault(c => c.Name == "Processing.Activity.Name")?.Value; 
      if (string.IsNullOrEmpty(activityName)) { throw new Exception("Activity without Processing.Activity.Name config");}

      // eseguo l'activity
      BaseActivity activityExecution = GetActivity(activityName);
      activityExecution.Setup(wire);
      nexts = activityExecution.Execution();*/

      // rimuovo eventuali duplicazioni
      nexts = nexts.Distinct().ToList();
      nexts.Remove("END");

      // se non ci sono prossimi nodi da lavorare posso chiudere il wire
      if (nexts.Count <= 0) {
        wire.State = "CLOSED";
        WiresService.Update(wire);
        return;
      }

      // altrimenti processo tutti i prossimi nodi
      bool isFirst = true;
      foreach (string next in nexts){
        // recupero il target dal link configurato per il nodo
        long? idTargetNode = wire.Node.OutputLinks.FirstOrDefault(l => l.Key != null && l.Key == next)?.IdTargetNode;
        if (!idTargetNode.HasValue) { throw new Exception("Link without target node"); }

        // recupero il prossimo nodo
        Node? node = NodesService.GetNodeById(idTargetNode.Value, ["Datas", "Activities", "Activities.ActivityDefinition", "Activities.Datas", "OutputLinks"]); //NodesService.GetNodeByKeyAndIdDistribution(next, wire.Instance.IdDistribution.Value, ["Configs", "Activities"]);
        if (node == null) { throw new Exception("Node not found: " + next); }

        Wire? currentWire = null;

        if (isFirst){
          isFirst = false;
          currentWire = wire;
          currentWire.Node = node;
          currentWire.IdNode = node.Id;
          WiresService.Update(currentWire);
        } else {
          currentWire = new Wire() {
            CreatedDateTime = DateTime.Now,
            IdInstance = wire.Instance.Id,
            Instance = wire.Instance,
            IdNode = node.Id,
            Node = node,
            State = "CREATED"
          };
          WiresService.Insert(currentWire);
        }

        if (node.Activities == null) { throw new Exception("Node Without Activities: " + node.Id); }

        // conto le activity automatiche nel prossimo nodo
        int automaticsCount = node.Activities.Where(a => 
          a.ActivityDefinition != null && 
          a.ActivityDefinition.HasFrontEnd.HasValue && 
          !a.ActivityDefinition.HasFrontEnd.Value
        ).Count();
        
        // se il prossimo nodo ha tutte activity automatiche vado avanti
        if (node.Activities.Count == automaticsCount){
          Processing(currentWire, data);
        }
        
      }

      wire.State = "PENDING";
      WiresService.Update(wire);
    }
    catch (Exception ex) {
      wire.State = "ERROR";
      wire.Reason = ex.Message;
      WiresService.Update(wire);
    }
  }

  private BaseActivity GetActivityByActivityDefinition(ActivityDefinition ad){
    if (string.IsNullOrEmpty(ad.Group)) { throw new Exception("Activity without ActivityDefinition.Group config");}
    if (string.IsNullOrEmpty(ad.Name)) { throw new Exception("Activity without ActivityDefinition.Name config");}

    BaseActivity? activity = null;
    if (ad.Group == "Takes"){
      if (ad.Name == "TakeQueue") { activity = new Activities.Takes.TakeQueue(); }
      else if (ad.Name == "TakeManual") { activity = new Activities.Takes.TakeManual(); }
    } else if (ad.Group == "Decisions"){
      if (ad.Name == "DecisionStandard") { activity = new Activities.Decisions.DecisionStandard(); }
    } else if (ad.Group == "Forms"){
      if (ad.Name == "FormJS") { activity = new Activities.Forms.FormJS(); }
    } 
    
    if (activity == null) { throw new Exception("Activity not Found with group-name: " + ad.Group + "-" + ad.Name); }
    return activity;
  }
}