using Newtonsoft.Json;

namespace It.Flowy.Camunda.Models.Tasklist;

public class TaskQuary {

  /// <summary>
  /// CREATED, COMPLETED, CANCELED
  /// </summary>
  [JsonProperty("state", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? State	{ get; set; }

  [JsonProperty("assigned", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public bool? Assigned	{ get; set; }

  [JsonProperty("assignee", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? Assignee	{ get; set; }

  [JsonProperty("taskDefinitionId", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? TaskDefinitionId	{ get; set; }

  [JsonProperty("candidateGroup", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? CandidateGroup	{ get; set; }

  [JsonProperty("candidateUser", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? CandidateUser	{ get; set; }

  [JsonProperty("processDefinitionKey", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? ProcessDefinitionKey	{ get; set; }

  [JsonProperty("processInstanceKey", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? ProcessInstanceKey	{ get; set; }

  [JsonProperty("pageSize", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public int PageSize	{ get; set; }

  [JsonProperty("followUpDate", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public TaskQuaryDatefilter? FollowUpDate	{ get; set; }

  [JsonProperty("dueDate", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public TaskQuaryDatefilter? DueDate	{ get; set; }

  [JsonProperty("taskVariables", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public List<TaskQuaryVariable>? TaskVariables { get; set; }

  [JsonProperty("tenantIds", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public List<string>? TenantIds { get; set; }

  [JsonProperty("sort", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public List<TaskQuaryOrder>? Sort { get; set; }

  [JsonProperty("searchAfter", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public List<string>? SearchAfter { get; set; }

  [JsonProperty("searchAfterOrEqual", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public List<string>? SearchAfterOrEqual { get; set; }

  [JsonProperty("searchBefore", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public List<string>? SearchBefore { get; set; }

  [JsonProperty("searchBeforeOrEqual", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public List<string>? SearchBeforeOrEqual { get; set; }
}

public class TaskQuaryDatefilter {

  [JsonProperty("from", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? From	{ get; set; }

  [JsonProperty("to", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? To	{ get; set; }
}

public class TaskQuaryVariable {

  [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? Name	{ get; set; }

  [JsonProperty("value", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? Value	{ get; set; }

  //eq
  [JsonProperty("operator", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? Operator	{ get; set; }
}

public class TaskQuaryOrder {

  // completionTime, creationTime, followUpDate, dueDate
  [JsonProperty("field", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? Field	{ get; set; }

  // ASC, DESC
  [JsonProperty("order", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? Order	{ get; set; }

}