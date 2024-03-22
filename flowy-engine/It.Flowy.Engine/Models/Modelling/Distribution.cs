using System.ComponentModel.DataAnnotations.Schema;

namespace It.Flowy.Engine.Models.Modelling;

[Table("Distributions", Schema = "Modelling")]
public class Distribution {
    public long? Id { get; set; }

    [ForeignKey(nameof(Process))]
    public long? IdProcess { get; set; }
    public Process? Process { get; set; }

    public int Version { get; set;}
    public bool IsEnabled { get; set; } = false;

    /// <summary>
    /// Rappresenta lo stato in cui si trova questo rilascio
    /// </summary>
    /// <value>
    /// DRAFT - Bozza
    /// TEST - Test
    /// PROD - Produzione
    /// </value>
    public string? State { get; set; }
    public DateTime CreatedDateTime { get; set; } = DateTime.Now;

    public ICollection<Node>? Nodes { get; set; }
}