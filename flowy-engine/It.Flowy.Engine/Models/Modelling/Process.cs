using System.ComponentModel.DataAnnotations.Schema;

namespace It.Flowy.Engine.Models.Modelling;

[Table("Processes", Schema = "Modelling")]
public class Process {
    public long? Id { get; set; }

    public string Key { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    
    public ICollection<Distribution>? Distributions { get; set; }
}