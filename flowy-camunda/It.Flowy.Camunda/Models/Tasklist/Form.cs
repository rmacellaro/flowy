using Newtonsoft.Json;

namespace It.Flowy.Camunda.Models.Tasklist;

public class Form {

  [JsonProperty("id")]
  public string? Id	{ get; set; }

  [JsonProperty("processDefinitionKey")]
  public string? ProcessDefinitionKey	{ get; set; }

  [JsonProperty("title")]
  public string? Title	{ get; set; }

  [JsonProperty("schema")]
  public string? Schema	{ get; set; }

  [JsonProperty("tenantId")]
  public string? TenantId	{ get; set; }
}