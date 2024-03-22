using System.ComponentModel.DataAnnotations.Schema;

namespace It.Flowy.Camunda.Models.Core.Modelling;

[Table("Processes", Schema = "Modelling")]
public class Process {
  public long Id { get; set; }

  [ForeignKey("Scope")]
  public long IdScope { get; set; }
  public Scope? Scope { get; set; }

  // camunda variables
  public long Key { get; set; }
  public string? Name { get; set; }
  public int Version { get; set; }
  public string? BpmnProcessId { get; set; }
  public string? TenantId { get; set; }
}