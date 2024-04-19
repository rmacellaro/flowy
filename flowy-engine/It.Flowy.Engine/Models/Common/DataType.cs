namespace It.Flowy.Engine.Models.Common;

public class DataType {
    public long? Id { get; set; }
    public string? Name { get; set; }
    public bool IsEnabled { get; set; }
    public string? Type { get; set; }
    public int? Index { get; set; }
    public string? DefaultValue { get; set; }
    public string? EditSettings { get; set; }
    public string? ShowSettings { get; set; }
    public string? DetailSettings { get; set; }
}