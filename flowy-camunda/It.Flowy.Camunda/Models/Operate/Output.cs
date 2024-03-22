using Newtonsoft.Json;

namespace It.Flowy.Camunda.Models.Operate;

public class Output {
  
  [JsonProperty("id")]
  public string? Id	{ get; set; }

  [JsonProperty("name")]
  public string? Name	{ get; set; }

  [JsonProperty("value")]
  public string? Value { get; set; }

  [JsonProperty("ruleId")]
  public string? RuleId	{ get; set; }

  [JsonProperty("ruleIndex")]
  public int RuleIndex	{ get; set; }
}