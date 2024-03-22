using Newtonsoft.Json;

namespace It.Flowy.Camunda.Models.Operate;

public class Incident {

  [JsonProperty("key")]
  public long Key { get; set; }

  [JsonProperty("processDefinitionKey")]
  public long ProcessDefinitionKey { get; set; }

  [JsonProperty("processInstanceKey")]
  public long ProcessInstanceKey { get; set; }

  /// <summary>
  /// UNSPECIFIED, UNKNOWN, IO_MAPPING_ERROR, JOB_NO_RETRIES, 
  /// CONDITION_ERROR, EXTRACT_VALUE_ERROR, CALLED_ELEMENT_ERROR, 
  /// UNHANDLED_ERROR_EVENT, MESSAGE_SIZE_EXCEEDED, CALLED_DECISION_ERROR, 
  /// DECISION_EVALUATION_ERROR
  /// </summary>
  [JsonProperty("type")]
  public string? Type { get; set; }

  [JsonProperty("message")]
  public string? Message { get; set; }

  [JsonProperty("creationTime")]
  public string? CreationTime { get; set; }

  /// <summary>
  /// ACTIVE, RESOLVED, PENDING 
  /// </summary>
  [JsonProperty("state")]
  public string? State { get; set; }

  [JsonProperty("tenantId")]
  public string? TenantId { get; set; }

}