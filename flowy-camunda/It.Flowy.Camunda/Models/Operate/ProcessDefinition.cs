using Newtonsoft.Json;

namespace It.Flowy.Camunda.Models.Operate;

public class ProcessDefinition {

  [JsonProperty("key")]
  public long Key { get; set; }

  [JsonProperty("name")]
  public string? Name { get; set; }

  [JsonProperty("version")]
  public int Version { get; set; }

  [JsonProperty("bpmnProcessId")]
  public string? BpmnProcessId { get; set; }

  [JsonProperty("tenantId")]
  public string? TenantId { get; set; }
}