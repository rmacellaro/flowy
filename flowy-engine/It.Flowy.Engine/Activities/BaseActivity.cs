using It.Flowy.Engine.Models.Modelling;
using It.Flowy.Engine.Models.Processing;
using Newtonsoft.Json;

namespace It.Flowy.Engine.Activities;

public abstract class BaseActivity {

    public Wire? CurrentWire { get; private set; }

    public virtual void Setup(Wire wire) {
        CurrentWire = wire;
    }

    public virtual List<string> Execution() {
        return [];
    }

    /*public long? GetTargetLinkByIndex(int index) {
        if (CurrentWire == null) { throw new Exception("Wire not found"); }
        if (CurrentWire.Node == null) { throw new Exception("Node not found"); }
        if (CurrentWire.Node.OutputLinks == null) { throw new Exception("Node not found"); }
        Link? link =  null;
        try{ link = CurrentWire.Node.OutputLinks.ToList()[index]; } catch {}
        return link?.IdTargetNode;
    }

    public long? GetTargetLinkByKey(string key){
        if (CurrentWire == null) { throw new Exception("Wire not found"); }
        if (CurrentWire.Node == null) { throw new Exception("Node not found"); }
        if (CurrentWire.Node.OutputLinks == null) { throw new Exception("Node not found"); }
        Link? link = CurrentWire.Node.OutputLinks.FirstOrDefault(o => o.Key == key);
        if (link == null) { throw new Exception("No out link by key");}
        return link.IdTargetNode;
    }*/
}