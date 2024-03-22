using It.Flowy.Camunda.Apis.Tasklist;
using It.Flowy.Camunda.Apis.Zeebe;
using It.Flowy.Camunda.Helpers;
using It.Flowy.Camunda.Models.Core.Modelling;
using It.Flowy.Camunda.Models.Core.Processing;
using It.Flowy.Camunda.Models.Tasklist;
using It.Flowy.Camunda.Services;
using Zeebe.Client.Api.Responses;

namespace It.Flowy.Camunda.Logic;

public interface IProcessingLogic {
  Instance? Start(long idProcess);
  InstanceTask GetInstanceTaskById(long idTask);
  Interaction GetInteractionByIdTask(long idTask);
}

public class ProcessingLogic : IProcessingLogic {

  private readonly IInstancesService InstancesService;
  private readonly IProcessesService ProcessesService;
  private readonly IZeebeApi ZeebeApi;
  private readonly ITasksApi TasksApi;
  private readonly IFormsApi FormsApi;
  private readonly IInteractionsService InteractionsService;

  public ProcessingLogic(
    IZeebeApi zeebeSrv,
    IProcessesService deploymentsSrv,
    IInstancesService instancesSrv,
    ITasksApi tasksService,
    IFormsApi formsService,
    IInteractionsService interactionsService
  ) {
    ZeebeApi = zeebeSrv;
    ProcessesService = deploymentsSrv;
    InstancesService = instancesSrv;
    TasksApi = tasksService;
    FormsApi = formsService;
    InteractionsService = interactionsService;
  }

  public Instance? Start(long idProcess) {
    string reference = Guid.NewGuid().ToString();
    // recupero prima il deployment
    Process? process = ProcessesService.GetProcessById(idProcess);
    if (process == null) { throw new Exception("Deployment not found by id: " + idProcess);}
    
    //creo una nuova istanza in camunda
    IProcessInstanceResponse response = ZeebeApi.CreateProcessInstance(process.Key);

    Instance instance = new() {
      CreatedAt = DateTime.Now,
      IdProcess = process.Id,
      Key = response.ProcessInstanceKey,
      Reference = reference
    };

    InstancesService.Insert(instance);
    return instance;
    /*
     public long IdProcess { get; set; }
  public Process? Process { get; set; }
  public long Key { get; set; }
  public DateTime CreatedAt { get; set; }
  public string? Reference { get; set; }
    */

  }

  public InstanceTask GetInstanceTaskById(long idTask) {
    // recupero il task da camunda
    It.Flowy.Camunda.Models.Tasklist.Task? task = TasksApi.GetTaskById(idTask.ToString());
    if (task == null) { throw new Exception("Task not found with id: " + idTask);}
    return MappingHelper.MappTask(task);
  }

  public Interaction GetInteractionByIdTask(long idTask) {
    It.Flowy.Camunda.Models.Tasklist.Task? task = TasksApi.GetTaskById(idTask.ToString());
    if (task == null) { throw new Exception("Task not found with id: " + idTask);}
    if (task.FormKey == null) { throw new Exception("Task Without formkey");}
    if (task.ProcessDefinitionKey == null) { throw new Exception("Task Without processdefinition");}
    Form? form = FormsApi.GetFormByIdAndProcessDefinition(task.FormKey, task.ProcessDefinitionKey);
    // se è stata trovata una form allora la prendo e la convero in Interaction per restituirla
    if (form != null) { return MappingHelper.MappInteraction(form);}
    // altrimenti vedo di recuperare l'interaction
    Interaction? interaction = InteractionsService.GetInteractionByName(task.FormKey);
    if (interaction == null) { throw new Exception("No interaction with name: " + task.FormKey);}
    return interaction;
  }

}