using Newtonsoft.Json;

namespace It.Flowy.Camunda.Models.Operate;

public class FlowNodeInstance {

  [JsonProperty("key", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public long Key { get; set; }
  
  [JsonProperty("processInstanceKey", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public long ProcessInstanceKey { get; set; }

  [JsonProperty("processDefinitionKey", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public long ProcessDefinitionKey { get; set; }

  [JsonProperty("startDate", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? StartDate { get; set; }

  [JsonProperty("endDate", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? EndDate { get; set; }

  [JsonProperty("flowNodeId", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? FlowNodeId { get; set; }

  [JsonProperty("flowNodeName", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? FlowNodeName { get; set; }

  [JsonProperty("incidentKey", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public long IncidentKey { get; set; }

  /// <summary>
  /// UNSPECIFIED, PROCESS, SUB_PROCESS, EVENT_SUB_PROCESS, START_EVENT, 
  /// INTERMEDIATE_CATCH_EVENT, INTERMEDIATE_THROW_EVENT, BOUNDARY_EVENT, 
  /// END_EVENT, SERVICE_TASK, RECEIVE_TASK, USER_TASK, MANUAL_TASK, TASK, 
  /// EXCLUSIVE_GATEWAY, INCLUSIVE_GATEWAY, PARALLEL_GATEWAY, EVENT_BASED_GATEWAY, 
  /// SEQUENCE_FLOW, MULTI_INSTANCE_BODY, CALL_ACTIVITY, BUSINESS_RULE_TASK, 
  /// SCRIPT_TASK, SEND_TASK, UNKNOWN
  /// </summary>
  [JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? Type { get; set; }

  /// <summary>
  /// ACTIVE, COMPLETED, TERMINATED
  /// </summary>
  [JsonProperty("state", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? State { get; set; }

  [JsonProperty("incident", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public bool Incident { get; set; }

  [JsonProperty("tenantId", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? TenantId { get; set; }
  
}