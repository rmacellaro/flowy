using System.ComponentModel.DataAnnotations.Schema;

namespace It.Flowy.Camunda.Models.Core.Processing;

[Table("InstanceTasks", Schema = "Processing")]
public class InstanceTask {
  public long? Id { get; set; }
  public string? Name { get; set; }
  public string? TaskDefinitionId { get; set; }
  public string? ProcessName { get; set; }
  public DateTime? CreationDate { get; set; }
  public DateTime? CompletionDate { get; set; }
  public string? Assignee { get; set; }
  public string? TaskState { get; set; }
  public string? FormKey { get; set; }
  public long? ProcessDefinitionKey { get; set; }
  public long? ProcessInstanceKey { get; set; }
  public string? TenantId { get; set; }
  public DateTime? DueDate { get; set; }
  public DateTime? FollowUpDate { get; set; }
  public List<string>? CandidateGroups { get; set; }
  public List<string>? CandidateUsers { get; set; }
}
