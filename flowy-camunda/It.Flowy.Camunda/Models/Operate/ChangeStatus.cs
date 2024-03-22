using Newtonsoft.Json;

namespace It.Flowy.Camunda.Models.Operate;

public class ChangeStatus {

  [JsonProperty("message")]
  public string? Message { get; set; }

  [JsonProperty("deleted")]
  public long Deleted { get; set; }
}