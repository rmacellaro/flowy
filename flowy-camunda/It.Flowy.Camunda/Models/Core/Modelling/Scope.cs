using System.ComponentModel.DataAnnotations.Schema;

namespace It.Flowy.Camunda.Models.Core.Modelling;

[Table("Scopes", Schema = "Modelling")]
public class Scope {
  public long Id { get; set; }
  public string? Name { get; set; }
  public string? Description { get; set; }
}