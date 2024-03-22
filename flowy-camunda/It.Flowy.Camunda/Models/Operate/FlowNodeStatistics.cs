using Newtonsoft.Json;

namespace It.Flowy.Camunda.Models.Operate;

public class FlowNodeStatistics {

  /// <summary>
  /// The id of the flow node for which the results are aggregated
  /// </summary>
  [JsonProperty("activityId")]
  public string? ActivityId { get; set; }

  /// <summary>
  /// The total number of active instances of the flow node
  /// </summary>
  [JsonProperty("active")]
  public long Active { get; set; }

  /// <summary>
  /// The total number of canceled instances of the flow node
  /// </summary>
  [JsonProperty("canceled")]
  public long Canceled { get; set; }

  /// <summary>
  /// The total number of incidents for the flow node
  /// </summary>
  [JsonProperty("incidents")]
  public long Incidents { get; set; }

  /// <summary>
  /// The total number of completed instances of the flow node
  /// </summary>
  [JsonProperty("completed")]
  public long Completed { get; set; }

}