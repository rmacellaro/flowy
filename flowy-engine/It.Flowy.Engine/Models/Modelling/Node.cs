using System.ComponentModel.DataAnnotations.Schema;

namespace It.Flowy.Engine.Models.Modelling;

[Table("Nodes", Schema = "Modelling")]
public class Node {
    public long? Id { get; set; }

    [ForeignKey(nameof(Distribution))]
    public long? IdDistribution { get; set; }
    public Distribution? Distribution { get; set; }

    public string Key { get; set; } = Guid.NewGuid().ToString();
    //public string Title { get; set; } = string.Empty;
    //public string? Description { get; set; }

    public ICollection<NodeData>? Datas { get; set; }
    public ICollection<Activity>? Activities { get; set; }

    [InverseProperty(nameof(Link.SourceNode))]
    public ICollection<Link>? OutputLinks { get; set;}

    [InverseProperty(nameof(Link.TargetNode))]
    public ICollection<Link>? InputLinks { get; set;}
}