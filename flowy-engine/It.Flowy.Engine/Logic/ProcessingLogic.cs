using It.Flowy.Engine.Activities;
using It.Flowy.Engine.Models.Modelling;
using It.Flowy.Engine.Models.Processing;
using It.Flowy.Engine.Services.Modelling;
using It.Flowy.Engine.Services.Processing;
using Newtonsoft.Json.Linq;

namespace It.Flowy.Engine.Logic;

public interface IProcessingLogic {
    List<Instance>? GetInstancesByIdProcess(long idProcess);
    Instance? GetInstanceByIdWire(long idWire);
    Node? GetStartNodeWithInteractionByIdDistribution(long idDistribution);
    Interaction? GetInteractionWithConfigurationsById(long idInteraction);
    Instance? Start(JObject request);
    Instance? Continue(JObject request);
}

public class ProcessingLogic(
    IProcessesService procSrv,
    IDistributionsService relSrv,
    INodesService nodSrv,
    IInteractionsService intSrv,
    IInstancesService instSrv,
    IWiresService wirSrv
) : IProcessingLogic {
    
    private readonly IProcessesService ProcessesService = procSrv;
    private readonly IDistributionsService DistributionsService = relSrv;
    private readonly INodesService NodesService = nodSrv;
    private readonly IInteractionsService InteractionsService = intSrv;
    private readonly IInstancesService InstancesService = instSrv;
    private readonly IWiresService WiresService = wirSrv;

    public List<Instance>? GetInstancesByIdProcess(long idProcess){
        return InstancesService.GetInstancesByIdProcess(idProcess, ["Wires", "Wires.Node", "Datas"]);
    }

    public Instance? GetInstanceByIdWire(long idWire) {
        Instance? i = InstancesService.GetInstanceByIdWire(idWire, ["Wires", "Datas"]);
        if (i != null && i.Wires != null){
            foreach(Wire w in i.Wires){
                if (w.IdNode != null){
                    w.Node = NodesService.GetNodeById(w.IdNode.Value, [ "Interactions" ]);
                }
            }
        }
        return i;
    }

    public Node GetStartNodeWithInteractionByIdDistribution(long idDistribution) {
        Node? startNode = NodesService.GetNodeByKeyAndIdDistribution("START", idDistribution, [ "Interactions"]);
        // se non trovo il nodo di start sollevo eccezione
        if (startNode == null) { throw new Exception("No Start Node to idDistribution: " + idDistribution );}
        if (startNode.Interactions == null || startNode.Interactions.Count <= 0) { throw new Exception("No Interaction to Start Node");}
        return startNode;
    }

    public Interaction? GetInteractionWithConfigurationsById(long idInteraction){
        Interaction? interaction = InteractionsService.GetInteractionById(idInteraction, true);
        if (interaction == null) { throw new Exception("No Interaction by id: " + idInteraction);}
        if (interaction.Configurations == null || interaction.Configurations.Count <= 0){ throw new Exception("No Configurations to Interaction");}
        // rimuovo dalla lista le configurazioni che sono destinate solo al motore di processo
        interaction.Configurations = interaction.Configurations.Where(
            c => c.IsForProcessingOnly.HasValue && !c.IsForProcessingOnly.Value
        ).ToList();
        return interaction;
    }

    public Instance? Start(JObject request) {
        long? idDistribution = request.GetValue("idDistribution")?.Value<long>();
        long? idProcess = request.GetValue("idProcess")?.Value<long>();
        long? idManualInteraction = request.GetValue("idInteraction")?.Value<long>();

        // se non è stato specificato ne un idDistribution ne un idProcess allora sollevo eccezione
        if (!idDistribution.HasValue && !idProcess.HasValue) { throw new Exception("Not Found idDistribution and idProcess"); }

        Distribution? distribution = null;
        // se è specificato un idDistribution allora recupero la distribuzione
        if (idDistribution.HasValue){ 
            distribution = DistributionsService.GetDistributionById(idDistribution.Value, null);
            // se non trovo la distribuzione sollevo eccezione
            if (distribution == null) { throw new Exception("Distribution not fount with idDistribution: " + idDistribution.Value); } 
            // se è una distirbuzione di test è l'utente non è abilitato per eseguirla sollevo eccezione   
        } else if (idProcess.HasValue){
            List<Distribution>? distributions = DistributionsService.GetDistributionsByIdProcess(idProcess.Value);
            if (distributions == null || distributions.Count <= 0){ throw new Exception("No Distributions by idProcess: " + idProcess.Value); }
            foreach(Distribution rel in distributions){
                if (!rel.IsEnabled) { continue; }
                if (rel.State == null){ continue; }
                if (rel.State.Equals("DRAFT")) { continue; }
                // se utente non abilitato al test sollevo eccezione
                //if (rel.State.Equals("TEST") )
                distribution = rel;
                break;
            }
            if (distribution == null) { throw new Exception("Distribution not found with idProcess: " + idProcess.Value);}
        }
        if (distribution == null || !distribution.Id.HasValue) { throw new Exception("Distribution not found");}

        // recupero il nodo di start per la distribution
        Node? start = NodesService.GetNodeByKeyAndIdDistribution("START", distribution.Id.Value);
        if (start == null || !start.Id.HasValue) { throw new Exception("Start Node not found by idDistribution: " + distribution.Id.Value);}

        // recupero tutte le interazioni per il nodo di start
        List<Interaction>? startInteractions = InteractionsService.GetInteractionsByIdNode(start.Id.Value, true);
        if (startInteractions == null || startInteractions.Count <= 0) { throw new Exception("Interactions not found by start node: " + start.Id.Value);}

        // se è stata specificata un interazione manuale
        if (idManualInteraction.HasValue) {
            // e l'interazione non è presente nelle interazioni del nodo
            if (startInteractions.Find(i => i.Id.Equals(idManualInteraction)) == null) {
                throw new Exception("Interaction not found with idInteraction: " + idManualInteraction.Value);
            }
        }

        // posso creare l'istanza è processare le interazioni 
        Instance newInstance = new () {
            CreatedDateTime = DateTime.Now,
            IdDistribution = distribution.Id,
            Distribution = distribution,
            Key = Guid.NewGuid().ToString()
        };
        InstancesService.Insert(newInstance);

        // creo un filo di elaborazione
        Wire wire = new (){
            IdInstance = newInstance.Id,
            Instance = newInstance,
            State = "STARTING",
            CreatedDateTime = DateTime.Now,
            UpdatedDateTime = DateTime.Now,
            IdNode = start.Id,
            Node = start
        };
        WiresService.Insert(wire);

        // processo
        Processing(new(){
            Wire = wire,
            IdManualInteraction = idManualInteraction,
            Data = request
        });
        return InstancesService.GetInstanceById(newInstance.Id, [
            "Wires",
            "Datas"
        ]);
    }

    public Instance? Continue(JObject request) {
        long? idWire = request.GetValue("idWire")?.Value<long>();
        long? idInstance = request.GetValue("idInstance")?.Value<long>();
        long? idManualInteraction = request.GetValue("idInteraction")?.Value<long>();

        if (!idManualInteraction.HasValue) { throw new Exception("idManualInteraction not found");}
        
        Wire? wire = null;
        if (idWire.HasValue) { 
            wire = WiresService.GetWireById(idWire.Value, true); 
            if (wire == null) { throw new Exception("Wire not found by id : " + idWire.Value); }
        }
        else if (idInstance.HasValue) { 
            Instance? instance = InstancesService.GetInstanceById(idInstance.Value, [ "Wires", "Datas" ]);
            if (instance == null) { throw new Exception("Instance not found by id : " + idInstance.Value); }
            if (instance.Wires == null) { throw new Exception("Instance.Wires not found by idInstance: " + idInstance.Value);}
            if (instance.Wires.Count > 1) { throw new Exception("Multiple Wires in instance: " + idInstance.Value);}
            var first = instance.Wires.First();
            if (!first.Id.HasValue){ throw new Exception("Instance Wire without id");}
            wire = WiresService.GetWireById(first.Id.Value, true);
        } else {
            throw new Exception("idWire or idInstance not found");
        }
        if (wire == null || wire.IdNode == null) { throw new Exception("Wire to processing not found"); }
        wire.Node = NodesService.GetNodeById(wire.IdNode.Value, [ "Interactions", "Interactions.Configurations"]);

        // processo
        Processing(new(){
            Wire = wire,
            IdManualInteraction = idManualInteraction,
            Data = request
        });

        return wire.Instance;
    }

    private void Processing(Elaboration e) {
        
        if (e.Wire == null) { throw new Exception("Wire not found in elaboration");}
        if (e.Wire.Instance == null) { throw new Exception("Wire without Instance: " + e.Wire.Id); }
        if (e.Wire.Instance.IdDistribution == null) { throw new Exception("wire.Instance.IdDistribution is null" + e.Wire.Instance.Id);}
        if (e.Wire.Node == null) { throw new Exception("Wire without Node : " + e.Wire.Id); }
        if (e.Wire.Node.Interactions == null) { throw new Exception("Wire.Node Without interactions " + e.Wire.Node.Id); }

        try{
            e.Wire.State = "PROCESSING";
            WiresService.Update(e.Wire);
            
            // preparo la lista dei prossimi nodi da eseguire
            List<string> nexts = [];

            // elaboro tutte le interazioni passate
            var interactions = e.Wire.Node.Interactions.OrderBy(i => i.Order).ToList();
            foreach(Interaction interaction in interactions) {
                if (!interaction.Type.Equals("AUTOMATIC")){
                    if (!e.IdManualInteraction.HasValue){ continue; }
                    if (!interaction.Id.Equals(e.IdManualInteraction.Value)) { continue; }
                }
                if (interaction.Configurations == null) { throw new Exception("No elaboration configuration"); }
                // recupero il nome dell'activity da eseguire in base all'interazione
                string? activityName = (interaction.Configurations.FirstOrDefault(c => 
                    c.Type != null &&
                    c.Type.Equals("System.BE") &&
                    c.Name != null &&
                    c.Name.Equals("Activity")
                )?.Value) ?? throw new Exception("SYSTEM.ACTIVITY attribute not found");
                // recupero l'activity da eseguire
                BaseActivity activity = GetActivity(activityName);
                e.CurrentInteraction = interaction;
                activity.Setup(
                    e.Wire,
                    e.Wire.Instance,
                    e.CurrentInteraction
                );

                var nex = activity.Execution();
                nexts.AddRange(nex);
            }

            // rimuovo eventuali duplicazioni
            nexts = nexts.Distinct().ToList();
            nexts.Remove("END");

            // se non ci sono prossimi nodi da lavorare posso chiudere il wire
            if (nexts.Count <= 0) {
                e.Wire.State = "CLOSED";
                WiresService.Update(e.Wire);
                return;
            }

            // altrimenti processo tutti i prossimi nodi
            bool isFirst = true;
            foreach(string next in nexts) {
                // recupero il prossimo nodo
                Node? node = NodesService.GetNodeByKeyAndIdDistribution(next, e.Wire.Instance.IdDistribution.Value, [ "Interactions"]);
                if (node == null ){ throw new Exception("Node not found: " + next); }

                Wire? currentWire = null;

                if (isFirst) {
                    isFirst = false;
                    currentWire = e.Wire;
                    currentWire.Node = node;
                    currentWire.IdNode = node.Id;
                    WiresService.Update(currentWire);
                } else {
                    currentWire = new Wire() {
                        CreatedDateTime = DateTime.Now,
                        IdInstance = e.Wire.Instance.Id,
                        Instance = e.Wire.Instance,
                        IdNode = node.Id,
                        Node = node,
                        State = "CREATED"
                    };
                    WiresService.Insert(currentWire);
                }

                // se il nodo contiene tutte interactions automatiche allora processo il nuovo nodo
                if (node.Interactions == null) { throw new Exception("Node Without interactions: " + node.Id );}
                if (node.Interactions.Count(i => i.Type.Equals("AUTOMATION")) == node.Interactions.Count) {
                    Processing(new (){
                        Wire = currentWire,
                        Data = e.Data
                    });
                }
            }            
        } catch (Exception ex) {
            e.Wire.State = "ERROR";
            e.Wire.Reason = ex.Message;
            WiresService.Update(e.Wire);
        }
    }

    private BaseActivity GetActivity(string name) {
        BaseActivity? activity = null;
        if (name.Equals("Takes.TakeQueue")) { activity = new Activities.Takes.TakeQueue(); }
        else if (name.Equals("Decisions.DecisionStandard")) { activity = new Activities.Decisions.DecisionStandard(); }
        if (activity == null) { throw new Exception("Activity not Found with name: " + name);}
        return activity;
    }
}