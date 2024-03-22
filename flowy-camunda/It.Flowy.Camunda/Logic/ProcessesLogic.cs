using It.Flowy.Camunda.Apis.Operate;
using It.Flowy.Camunda.Models.Core.Modelling;
using It.Flowy.Camunda.Models.Operate;
using It.Flowy.Camunda.Services;
using log4net;

namespace It.Flowy.Camunda.Logic;

public interface IProcessesLogic {
  ICollection<Process>? GetProcessesByIdScope(long idScope);
  ICollection<FlowNodeStatistics>? GetStatisticsByIdProcess(long idProcess);
  string? GetSchemaByIdProcess(long idProcess);
}

public class ProcessesLogic : IProcessesLogic {
  private static readonly ILog Log = LogManager.GetLogger(typeof(ProcessesLogic));
  private readonly IProcessesService ProcessesService;
  private readonly IProcessDefinitionsApi ProcessDefinitionsApi;
  private readonly IProcessInstancesApi ProcessInstancesApi;

  public ProcessesLogic(
    IProcessesService prs,
    IProcessDefinitionsApi pds,
    IProcessInstancesApi pis
  ){
    ProcessesService = prs;
    ProcessDefinitionsApi = pds;
    ProcessInstancesApi = pis;
  }

  public ICollection<Process>? GetProcessesByIdScope(long idScope){
    // recupero tutti i deployment archiviati per lo scope indicato
    ICollection<Process>? deployments = ProcessesService.GetProcessesByIdScope(idScope);
    if (deployments == null) { return null; }
    
    // per ogni deployments inderrogo camunda per recuperarmi il dettaglio
    /*foreach(Process deployment in deployments) {
      deployment.ProcessDefinition = ProcessDefinitionsService.GetProcessDefinitionByKey(deployment.Key);   
    }*/
    return deployments;
  }

  public ICollection<FlowNodeStatistics>? GetStatisticsByIdProcess(long idProcess){
    // recupero la deployment specifica per sapre la keyprocessdefinition di camunda
    Process? deployment = ProcessesService.GetProcessById(idProcess);
    if (deployment == null) { return null; }
    // inderrogo camunda per le statistiche sulle istanze del deployment
    return ProcessInstancesApi.GetProcessInstancesStatisticsByProcessDefinition(deployment.Key);
  }
  
  public string? GetSchemaByIdProcess(long idProcess) {
    // recupero la deployment specifica per sapre la keyprocessdefinition di camunda
    Process? deployment = ProcessesService.GetProcessById(idProcess);
    if (deployment == null) { throw new Exception("No Process with id: " + idProcess); }
    // recupero lo schema del processo
    return ProcessDefinitionsApi.GetProcessDefinitionSchemaByKey(deployment.Key);
  }
  
}