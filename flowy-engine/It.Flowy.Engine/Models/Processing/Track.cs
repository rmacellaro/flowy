using System.ComponentModel.DataAnnotations.Schema;

namespace It.Flowy.Engine.Models.Processing;

[Table("Tracks", Schema = "Processing")]
public class Track {
    public long? Id { get; set; }

    [ForeignKey(nameof(Wire))]
    public long? IdWire { get; set; }
    public Wire? Wire { get; set; }

    public string? Message { get; set; }
    public string? Data { get; set; }

    public DateTime StartedDateTime { get; set; } = DateTime.Now;
    public DateTime EndedDateTime { get; set; } = DateTime.Now;
}