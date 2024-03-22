using It.Flowy.Camunda.Apis.Zeebe;
using It.Flowy.Camunda.Models.Core.Modelling;
using It.Flowy.Camunda.Services;
using log4net;
using Zeebe.Client.Api.Responses;

namespace It.Flowy.Camunda.Logic;

public interface IDraftsLogic {
  ICollection<Draft>? GetDraftsByIdScope(long idScope);
  Draft? GetDraftById(long idDraft);
  ICollection<DraftTrack>? GetDraftTracksByIdDraft(long idDraft);
  void UpdateDraftSchema(Draft draft);
  void UpdateDraftInfo(Draft draft);
  Draft CloneDraft(long idDraft);
  Draft NewDraft(Draft draft);
  List<Process>? DeployDraft(long idDraft);
}

public class DraftsLogic : IDraftsLogic {
  private static readonly ILog Log = LogManager.GetLogger(typeof(DraftsLogic));
  private readonly IDraftsService DraftsService;
  private readonly IProcessesService DeploymentsService;
  private readonly IZeebeApi ZeebeApi;

  public DraftsLogic(
    IDraftsService ds,
    IProcessesService dds,
    IZeebeApi zs
  ){
    DraftsService = ds;
    DeploymentsService = dds;
    ZeebeApi = zs;
  }

  public ICollection<Draft>? GetDraftsByIdScope(long idScope){
    Log.Debug("START idScope:" + idScope);
    return DraftsService.GetDraftsByIdScope(idScope);
  }

  public Draft? GetDraftById(long idDraft) {
    Log.Debug("START idDraft:" + idDraft);
    return DraftsService.GetDraftById(idDraft);
  }

  public ICollection<DraftTrack>? GetDraftTracksByIdDraft(long idDraft){
    Log.Debug("START idDraft:" + idDraft);
    return DraftsService.GetDraftTracksByIdDraft(idDraft);
  }

  public void UpdateDraftSchema(Draft draft) {
    // recupero la bozza dal database
    Draft? draftDb = DraftsService.GetDraftById(draft.Id);
    if (draftDb == null) { throw new Exception("Draft with id : " + draft.Id + ", not found!");}
    // aggiorno lo schema
    string? oldSchema = draftDb.Schema;
    draftDb.Schema = draft.Schema;
    DraftsService.UpdateDraft(draftDb);
    // aggiungo un a tracciatura
    DraftsService.InsertDraftTrack(new (){
      IdDraft = draftDb.Id,
      Draft = draftDb,
      EventAt = DateTime.Now,
      SchemaBackup = oldSchema,
      Operation = "UPDATE_DRAFT_SCHEMA"
    });
  }

  public void UpdateDraftInfo(Draft draft){
    // recupero la bozza dal database
    Draft? draftDb = DraftsService.GetDraftById(draft.Id);
    if (draftDb == null) { throw new Exception("Draft with id : " + draft.Id + ", not found!");}
    string oldValues = "Valori precedenti: " + draft.Name + ";" + draft.Description;
    // aggiorno lo schema
    draftDb.Name = draft.Name;
    draftDb.Description = draft.Description;
    DraftsService.UpdateDraft(draftDb);
    // aggiungo un a tracciatura
    DraftsService.InsertDraftTrack(new (){
      IdDraft = draftDb.Id,
      Draft = draftDb,
      EventAt = DateTime.Now,
      Operation = "UPDATE_DRAFT_INFO",
      Description = oldValues
    });
  }

  public Draft CloneDraft(long idDraft) {
    // recupero la bozza dal database
    Draft? draft = DraftsService.GetDraftById(idDraft);
    if (draft == null) { throw new Exception("Draft with id : " + idDraft + ", not found!");}

    // creo un nuovo draft
    Draft newDraft = new () {
      Name = draft.Name + " (Copy)",
      Description = draft.Description,
      IdScope = draft.IdScope,
      Schema = draft.Schema
    };
    DraftsService.InsertDraft(newDraft);

    // aggiungo una tracciatura
    DraftsService.InsertDraftTrack(new (){
      IdDraft = newDraft.Id,
      Draft = newDraft,
      EventAt = DateTime.Now,
      Operation = "CLONE_DRAFT",
      Description = "Clonato da : " + draft.Name + " [" + draft.Id+ "]"
    });

    return newDraft;
  }

  public Draft NewDraft(Draft draft) {
    DraftsService.InsertDraft(draft);

    // aggiungo una tracciatura
    DraftsService.InsertDraftTrack(new (){
      IdDraft = draft.Id,
      Draft = draft,
      EventAt = DateTime.Now,
      Operation = "NEW_DRAFT"
    });

    return draft;
  }

  public List<Process>? DeployDraft(long idDraft){
    // recupero la bozza dal database
    Draft? draft = DraftsService.GetDraftById(idDraft);
    if (draft == null) { throw new Exception("Draft with id : " + idDraft + ", not found!");}
    if (draft.Schema == null) { throw new Exception("no schema in Draft");}

    // inizializzo la risposta
    List<Process> result = new ();
    // provo a fare il deploy della bozza in camunda
    IDeployResourceResponse response = ZeebeApi.Deploy(draft.Schema);
    // recupero la chiave del processo deployato
    if (response.Processes == null || response.Processes.Count <= 0) { throw new Exception("Error deploy schema on Camunda");}
    foreach(IProcessMetadata processMetadata in response.Processes){
      // recupero la chiave del processo deployato e verifico se è presente nel database per lo scope
      Process? process = DeploymentsService.GetProcessInScopeByKeyProcessDefinition(draft.IdScope, processMetadata.ProcessDefinitionKey);
      // se il processo è già presente come deployment non lo aggiungo di nuovo
      if (process != null) { continue; }
      // aggiungo il deployment per lo scope
      Process newDeployment = new () {
        IdScope = draft.IdScope,
        Key = processMetadata.ProcessDefinitionKey,
        BpmnProcessId = processMetadata.BpmnProcessId,
        Name = processMetadata.ResourceName,
        Version = processMetadata.Version
      };
      DeploymentsService.InsertProcess(newDeployment);
      result.Add(newDeployment);
      // aggiungo una tracciatura alla bozza
      DraftsService.InsertDraftTrack(new (){
        IdDraft = draft.Id,
        Draft = draft,
        EventAt = DateTime.Now,
        Operation = "DEPLOY_DRAFT",
        Description = "Deployment id: " + newDeployment.Id
      });
    }

    if (result.Count > 0 ) { return result; }
    return null;
  }
}