using System.ComponentModel.DataAnnotations.Schema;

namespace It.Flowy.Engine.Models.Modelling;

[Table("Nodes", Schema = "Modelling")]
public class Node {
    public long? Id { get; set; }

    [ForeignKey(nameof(Distribution))]
    public long? IdDistribution { get; set; }
    public Distribution? Distribution { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Key { get; set; } = Guid.NewGuid().ToString();
    public string? Description { get; set; }
    public string? Color { get; set; }
    public double? Percentage { get; set; }

    public ICollection<Interaction>? Interactions { get; set; }
}