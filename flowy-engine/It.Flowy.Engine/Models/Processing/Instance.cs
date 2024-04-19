using System.ComponentModel.DataAnnotations.Schema;
using It.Flowy.Engine.Models.Modelling;

namespace It.Flowy.Engine.Models.Processing;

/// <summary>
/// Un istanza di lavorazione è una pratica che si 
/// sta lavorando per un determinato processo
/// </summary>
[Table("Instances", Schema = "Processing")]
public class Instance {
    public long? Id { get; set; }

    [ForeignKey(nameof(Distribution))]
    public long? IdDistribution { get; set; }
    public Distribution? Distribution { get; set; }

    public string Key { get; set; } = Guid.NewGuid().ToString();
    public DateTime CreatedDateTime { get; set; } = DateTime.Now;

    /// <summary>
    /// Fili di lavorazione per questa istanza
    /// </summary>
    /// <value></value>
    public ICollection<Wire>? Wires { get; set; }

    /// <summary>
    /// Sono i Dati associati a questa istanza di lavorazione
    /// </summary>
    /// <value></value>
    public ICollection<InstanceData>? Datas { get; set; }
}