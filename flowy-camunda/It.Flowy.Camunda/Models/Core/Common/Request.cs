namespace It.Flowy.Camunda.Models.Core.Common;

public class Request {
  public int Offset { get; set; }
  public int Size { get; set; }
  public Sort? Sort { get; set;}
  public List<Query>? Queries { get; set; }
  public List<object>? SearchAfter { get; set; }
}