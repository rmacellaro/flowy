using System.ComponentModel.DataAnnotations.Schema;
using It.Flowy.Engine.Models.Common;

namespace It.Flowy.Engine.Models.Modelling;

[Table("ActivityDefinitionDataTypes", Schema = "Modelling")]
public class ActivityDefinitionDataType : DataType {

  [ForeignKey(nameof(ActivityDefinition))]
  public long? IdActivityDefinition { get; set; }
  public ActivityDefinition? ActivityDefinition { get; set; }
}