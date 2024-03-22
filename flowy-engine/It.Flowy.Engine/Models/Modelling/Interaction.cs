using System.ComponentModel.DataAnnotations.Schema;

namespace It.Flowy.Engine.Models.Modelling;

[Table("Interactions", Schema = "Modelling")]
public class Interaction {
    public long? Id { get; set; }

    [ForeignKey(nameof(Node))]
    public long? IdNode { get; set; }
    public Node? Node { get; set; }

    //public bool IsDefault { get; set; }

    /// <summary>
    /// Indica il tipo di interazione possibile per 
    /// il nodo in cui si trova l'istanza
    /// </summary>
    /// <value>
    /// AUTOMATIC : interazione automatica: significa che esegui questa interazione automaticamente quando arrivo nel nodo senza passare per l'interfaccia
    /// DEFAULT : interazione manuale: predefinita
    /// OPTIONAL : interazione manuale: opzionale
    /// </value>
    public string Type { get; set; } = "DEFAULT";

    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Order { get; set; }
    /*public string? Icon { get; set; }
    public string Operation { get; set; } = string.Empty;
    public string? Data { get; set; }
    public string? Nexts { get; set; }*/

    public ICollection<Configuration>? Configurations { get; set; }
}