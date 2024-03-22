using Newtonsoft.Json;

namespace It.Flowy.Camunda.Models.Operate;

public class ProcessInstance {

  [JsonProperty("key", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public long? Key	{get; set;}

  [JsonProperty("processVersion", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public int? ProcessVersion	{get; set;}

  [JsonProperty("bpmnProcessId", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? BpmnProcessId {get; set;}

  [JsonProperty("parentKey", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public long? ParentKey {get; set;}

  [JsonProperty("parentFlowNodeInstanceKey", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public long? ParentFlowNodeInstanceKey {get; set;}

  [JsonProperty("startDate", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? StartDate {get; set;}

  [JsonProperty("endDate", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? EndDate {get; set;}

  /// <summary>
  /// ACTIVE, COMPLETED, CANCELED
  /// </summary>
  [JsonProperty("state", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? State {get; set;}
  
  [JsonProperty("processDefinitionKey", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public long? ProcessDefinitionKey {get; set;}

  [JsonProperty("tenantId", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? TenantId  {get; set;}

  [JsonProperty("parentProcessInstanceKey", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public object? ParentProcessInstanceKey {get; set;}
}