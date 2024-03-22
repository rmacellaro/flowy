using Newtonsoft.Json;

namespace It.Flowy.Camunda.Models.Operate;

public class Quary<T> {

  [JsonProperty("filter", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public T? Filter {get; set;}

  [JsonProperty("size", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public int Size { get; set; }

  [JsonProperty("searchAfter", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public List<object>? SearchAfter { get; set; }

  [JsonProperty("sort", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public Sort? Sort { get; set; }
}

public class Sort {

  [JsonProperty("field", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? Field { get; set; }

  /// <summary>
  /// ASC, DESC 
  /// </summary>
  [JsonProperty("order", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? Order { get; set; }
}

public class Results<T> {
  [JsonProperty("items")]
  public List<T>? Items { get; set; }

  [JsonProperty("sortValues")]
  public List<object>? SortValues { get; set; }

  [JsonProperty("total")]
  public int Total { get; set; }
}