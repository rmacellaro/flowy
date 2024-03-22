using System.ComponentModel.DataAnnotations.Schema;

namespace It.Flowy.Engine.Models.Processing;

[Table("Datas", Schema = "Processing")]
public class Data {
    public long? Id { get; set; }

    [ForeignKey(nameof(Instance))]
    public long? IdInstance { get; set; }
    public Instance? Instance { get; set; }

    public string? Name { get; set; }
    public string? Value { get; set; }
}