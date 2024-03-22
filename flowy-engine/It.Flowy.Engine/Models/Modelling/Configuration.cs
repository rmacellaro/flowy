using System.ComponentModel.DataAnnotations.Schema;

namespace It.Flowy.Engine.Models.Modelling;

[Table("Configurations", Schema = "Modelling")]
public class Configuration {
    public long? Id { get; set; }

    [ForeignKey(nameof(Interaction))]
    public long? IdInteraction { get; set; }
    public Interaction? Interaction { get; set; }

    public bool? IsForProcessingOnly { get; set; }
    public string? Type { get; set; }
    public string? Name { get; set; }
    public string? Value { get; set; }

    /*
    
    public string? Icon { get; set; }
    public string Operation { get; set; } = string.Empty;
    public string? Data { get; set; }
    public string? Nexts { get; set; }*/
}