using System.ComponentModel.DataAnnotations.Schema;

namespace It.Flowy.Camunda.Models.Core.Modelling;

[Table("Drafts", Schema = "Modelling")]
public class Draft {
  public long Id { get; set; }

  [ForeignKey("Scope")]
  public long IdScope { get; set; }
  public Scope? Scope { get; set; }

  public string? Name { get; set; }
  public string? Description { get; set; }
  public string? Schema { get; set; }
}