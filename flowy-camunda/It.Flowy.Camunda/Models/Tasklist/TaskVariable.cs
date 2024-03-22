using Newtonsoft.Json;

namespace It.Flowy.Camunda.Models.Tasklist;

public class TaskVariable {

  [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? Id { get; set; }

  [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? Name { get; set; }

  [JsonProperty("value", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? Value { get; set; }

  [JsonProperty("draft", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public object? Draft { get; set; }

  [JsonProperty("tenantId", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? TenantId { get; set; }
}