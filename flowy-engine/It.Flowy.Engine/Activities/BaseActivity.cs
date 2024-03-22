using It.Flowy.Engine.Models.Modelling;
using It.Flowy.Engine.Models.Processing;
using Newtonsoft.Json;

namespace It.Flowy.Engine.Activities;

public abstract class BaseActivity {

    public Wire? CurrentWire { get; private set; }
    public Instance? CurrentInstance { get; private set;}
    public Interaction? CurrentInteraction { get; private set;}

    public virtual void Setup(
        Wire wire,
        Instance instance,
        Interaction interaction
    ) {
        CurrentWire = wire;
        CurrentInstance = instance;
        CurrentInteraction = interaction;
    }

    public virtual List<string> Execution() {
        return ["NEXT"];
    }

    public List<string> GetNextsFromInteractionConfigurations(string type = "System.BE", string name = "Nexts"){
        if (CurrentInteraction == null) { throw new Exception("Interaction not found"); }
        if (CurrentInteraction.Configurations == null || CurrentInteraction.Configurations.Count <= 0) { throw new Exception("Configurations not found in interaction: " + CurrentInteraction.Id);}
        Configuration? config = CurrentInteraction.Configurations.FirstOrDefault(c => c.Type != null && c.Type.Equals(type) && c.Name != null && c.Name.Equals(name));
        if (config == null || config.Value == null) { throw new Exception("Configuration System.BE.Nexts not found");}
        List<string>? lst =  JsonConvert.DeserializeObject<List<string>>(config.Value);
        if(lst == null) { throw new Exception("Configuration System.BE.Nexts incorrect value");}
        return lst;
    }
}