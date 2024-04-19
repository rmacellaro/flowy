using System.ComponentModel.DataAnnotations.Schema;

namespace It.Flowy.Engine.Models.Modelling;

[Table("Links", Schema = "Modelling")]
public class Link {
    public long? Id { get; set; }

    [ForeignKey(nameof(SourceNode))]
    public long? IdSourceNode { get; set; }
    public Node? SourceNode { get; set; }

    public string? Key { get; set; }

    [ForeignKey(nameof(TargetNode))]
    public long? IdTargetNode { get; set; }
    public Node? TargetNode { get; set; }
}