using Newtonsoft.Json;

namespace It.Flowy.Camunda.Models.Tasklist;

public class Task {

  [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? Id	{ get; set; }

  [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? Name	{ get; set; }

  [JsonProperty("taskDefinitionId", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? TaskDefinitionId	{ get; set; }
  
  [JsonProperty("processName", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? ProcessName	{ get; set; }

  [JsonProperty("creationDate", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? CreationDate	{ get; set; }

  [JsonProperty("completionDate", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? CompletionDate	{ get; set; }

  [JsonProperty("assignee", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? Assignee	{ get; set; }

  /// <summary>
  /// CREATED, COMPLETED, CANCELED
  /// </summary>
  [JsonProperty("taskState", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? TaskState	{ get; set; }

  [JsonProperty("formKey", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? FormKey	{ get; set; }

  [JsonProperty("processDefinitionKey", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? ProcessDefinitionKey	{ get; set; }

  [JsonProperty("processInstanceKey", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? ProcessInstanceKey	{ get; set; }
  
  [JsonProperty("tenantId", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? TenantId	{ get; set; }

  [JsonProperty("dueDate", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? DueDate	{ get; set; }

  [JsonProperty("followUpDate", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? FollowUpDate	{ get; set; }

  [JsonProperty("candidateGroups", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public List<string>? CandidateGroups	{ get; set; }

  [JsonProperty("candidateUsers", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public List<string>? CandidateUsers	{ get; set; }
  
  [JsonProperty("sortValues", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public List<string>? SortValues {get; set;}

  [JsonProperty("isFirst", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public bool? IsFirst {get; set;}
}