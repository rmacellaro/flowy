using System.ComponentModel.DataAnnotations.Schema;
using It.Flowy.Engine.Models.Common;

namespace It.Flowy.Engine.Models.Processing;

[Table("InstanceDatas", Schema = "Processing")]
public class InstanceData : Data{

    [ForeignKey(nameof(Instance))]
    public long? IdInstance { get; set; }
    public Instance? Instance { get; set; }
}