using It.Flowy.Camunda.Models.Core.Modelling;
using It.Flowy.Camunda.Models.Core.Processing;
using It.Flowy.Camunda.Models.Operate;
using It.Flowy.Camunda.Models.Tasklist;

namespace It.Flowy.Camunda.Helpers;

public static class MappingHelper {

  public static InstanceTrack MappTrack(FlowNodeInstance flowNodeInstance) {
    return new InstanceTrack() {
      KeyFlowNodeInstance = flowNodeInstance.Key,
      EventAt = flowNodeInstance.StartDate != null ? DateTime.Parse(flowNodeInstance.StartDate) : null,
      StartDate = flowNodeInstance.StartDate != null ? DateTime.Parse(flowNodeInstance.StartDate) : null,
      EndDate = flowNodeInstance.EndDate != null ? DateTime.Parse(flowNodeInstance.EndDate) : null,
      FlowNodeId = flowNodeInstance.FlowNodeId,
      FlowNodeName = flowNodeInstance.FlowNodeName,
      Type = flowNodeInstance.Type,
      State = flowNodeInstance.State,
      Incident = flowNodeInstance.Incident,
      TenantId = flowNodeInstance.TenantId
    };
  }

  public static InstanceTask MappTask(It.Flowy.Camunda.Models.Tasklist.Task camundaTask) {
    return new InstanceTask() {
      Id = camundaTask.Id != null ? long.Parse(camundaTask.Id) : null,
      Name = camundaTask.Name,
      TaskDefinitionId = camundaTask.TaskDefinitionId,
      ProcessName = camundaTask.ProcessName,
      CreationDate = camundaTask.CreationDate != null ? DateTime.Parse(camundaTask.CreationDate) : null,
      CompletionDate = camundaTask.CompletionDate != null ? DateTime.Parse(camundaTask.CompletionDate) : null,
      Assignee = camundaTask.Assignee,
      TaskState = camundaTask.TaskState,
      FormKey = camundaTask.FormKey,
      ProcessDefinitionKey = camundaTask.ProcessDefinitionKey != null ? long.Parse(camundaTask.ProcessDefinitionKey) : null,
      ProcessInstanceKey = camundaTask.ProcessInstanceKey != null ? long.Parse(camundaTask.ProcessInstanceKey) : null,
      TenantId = camundaTask.TenantId,
      DueDate = camundaTask.DueDate != null ? DateTime.Parse(camundaTask.DueDate) : null,
      FollowUpDate = camundaTask.FollowUpDate != null ? DateTime.Parse(camundaTask.FollowUpDate) : null,
      CandidateGroups = camundaTask.CandidateGroups,
      CandidateUsers = camundaTask.CandidateUsers
    };
  }

  public static Interaction MappInteraction(Form form) {
    return new Interaction() {
      Name = form.Id,
      Type = "camunda-form",
      Data = form.Schema
    };
  }
}