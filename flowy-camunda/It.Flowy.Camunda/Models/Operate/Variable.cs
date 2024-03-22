using Newtonsoft.Json;

namespace It.Flowy.Camunda.Models.Operate;

public class Variable {

  [JsonProperty("key", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public long Key { get; set; }

  [JsonProperty("processInstanceKey", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public long ProcessInstanceKey { get; set; }

  [JsonProperty("scopeKey", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public long ScopeKey { get; set; }

  [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? Name { get; set; }

  [JsonProperty("value", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? Value { get; set; }

  [JsonProperty("truncated", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public bool Truncated { get; set; }

  [JsonProperty("tenantId", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? TenantId { get; set; }

}