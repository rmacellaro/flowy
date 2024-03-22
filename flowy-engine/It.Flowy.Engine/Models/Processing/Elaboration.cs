using It.Flowy.Engine.Models.Modelling;
using Newtonsoft.Json.Linq;

namespace It.Flowy.Engine.Models.Processing;

public class Elaboration {
    public Wire? Wire { get; set; } 
    public long? IdManualInteraction { get; set; }
    public JObject? Data { get; set; }
    public Interaction? CurrentInteraction { get; set; }
}