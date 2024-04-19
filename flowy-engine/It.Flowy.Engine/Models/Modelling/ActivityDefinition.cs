using System.ComponentModel.DataAnnotations.Schema;

namespace It.Flowy.Engine.Models.Modelling;

[Table("ActivityDefinitions", Schema = "Modelling")]
public class ActivityDefinition {
  public long? Id { get; set; }
  public string? Group { get; set; }
  public string? Name { get; set; }
  public bool? HasFrontEnd { get; set; }

  public ICollection<ActivityDefinitionDataType>? DataTypes { get; set; }
}