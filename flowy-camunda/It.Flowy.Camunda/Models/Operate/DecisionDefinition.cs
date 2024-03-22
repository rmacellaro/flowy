using Newtonsoft.Json;

namespace It.Flowy.Camunda.Models.Operate;

public class DecisionDefinition {
  [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? Id	{get; set; }

  [JsonProperty("key", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public long Key	{get; set; }

  [JsonProperty("decisionId", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? DecisionId {get; set; }

  [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? Name {get; set; }

  [JsonProperty("version", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public int Version {get; set; }

  [JsonProperty("decisionRequirementsId", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? DecisionRequirementsId {get; set; }

  [JsonProperty("decisionRequirementsKey", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public long DecisionRequirementsKey {get; set; }

  [JsonProperty("decisionRequirementsName", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? DecisionRequirementsName {get; set; }

  [JsonProperty("decisionRequirementsVersion", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public int DecisionRequirementsVersion {get; set; }

  [JsonProperty("tenantId", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? TenantId {get; set; }
}