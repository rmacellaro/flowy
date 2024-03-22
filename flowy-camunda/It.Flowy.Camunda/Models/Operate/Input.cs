using Newtonsoft.Json;

namespace It.Flowy.Camunda.Models.Operate;

public class Input {

  [JsonProperty("id")]
  public string? Id	{ get; set;}

  [JsonProperty("name")]
  public string? Name	{ get; set; }

  [JsonProperty("value")]
  public string? Value	{ get; set; }
}