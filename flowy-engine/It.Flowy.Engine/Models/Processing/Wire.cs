using System.ComponentModel.DataAnnotations.Schema;
using It.Flowy.Engine.Models.Modelling;

namespace It.Flowy.Engine.Models.Processing;

/// <summary>
/// Rappresenta un filo di un istanza
/// per ogni istanza posso avere più fili, 
/// per esempio per le operazioni parallele 
/// su di un istanza
/// </summary>
[Table("Wires", Schema = "Processing")]
public class Wire {
    public long? Id { get; set; }

    /// <summary>
    /// l'istanza alla quale si riferisce questo filo
    /// </summary>
    /// <value></value>
    [ForeignKey(nameof(Instance))]
    public long? IdInstance { get; set; }
    public Instance? Instance { get; set; }

    /// <summary>
    /// CREATED
    /// PROCESSING
    /// CLOSED
    /// ERROR
    /// </summary>
    /// <value></value>
    public string? State { get; set; }
    public string? Reason { get; set; }

    /// <summary>
    /// il nodo in cui si trova il filo di lavorazione
    /// </summary>
    /// <value></value>
    [ForeignKey(nameof(Node))]
    public long? IdNode { get; set; }
    public Node? Node { get; set; }
    
    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    public DateTime UpdatedDateTime { get; set; } = DateTime.Now;
}