using System.ComponentModel.DataAnnotations.Schema;

namespace It.Flowy.Engine.Models.Modelling;

[Table("Activities", Schema = "Modelling")]
public class Activity {
    public long? Id { get; set; }

    [ForeignKey(nameof(ActivityDefinition))]
    public long? IdActivityDefinition { get; set; }
    public ActivityDefinition? ActivityDefinition { get; set; }

    [ForeignKey(nameof(Node))]
    public long? IdNode { get; set; }
    public Node? Node { get; set; }

    public string? Key { get; set; }
    public int? Index { get; set; }

    public ICollection<ActivityData>? Datas { get; set; }
}