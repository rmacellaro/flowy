using System.ComponentModel.DataAnnotations.Schema;
using It.Flowy.Engine.Models.Common;

namespace It.Flowy.Engine.Models.Modelling;

[Table("ActivityDatas", Schema = "Modelling")]
public class ActivityData : Data{
    
    [ForeignKey(nameof(Activity))]
    public long? IdActivity { get; set; }
    public Activity? Activity { get; set; }
    

    [ForeignKey(nameof(ActivityDefinitionDataType))]
    public long? IdActivityDefinitionDataType { get; set; }
    public ActivityDefinitionDataType? ActivityDefinitionDataType { get; set; }
}