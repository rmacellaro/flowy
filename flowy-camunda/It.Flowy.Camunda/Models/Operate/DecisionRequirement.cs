using Newtonsoft.Json;

namespace It.Flowy.Camunda.Models.Operate;

public class DecisionRequirement {
  
  [JsonProperty("id")]
  public string? Id	{get; set; }

  [JsonProperty("key")]
  public long Key	{get; set; }

  [JsonProperty("decisionRequirementsId")]
  public long DecisionRequirementsId	{get; set; }

  [JsonProperty("name")]
  public string? Name {get; set; }

  [JsonProperty("version")]
  public int Version {get; set; }

  [JsonProperty("resourceName")]
  public string? ResourceName {get; set; }

  [JsonProperty("tenantId")]
  public string? TenantId {get; set; }

}