namespace It.Flowy.Camunda.Models.Core.Common;

public class Result<T> {
  public Request? Request { get; set; }
  public long Total { get; set; }
  public ICollection<T>? Items { get; set; }
  public List<object>? SortValues { get; set; }
}