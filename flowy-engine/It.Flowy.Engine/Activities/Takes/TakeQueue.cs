namespace It.Flowy.Engine.Activities.Takes;

public class TakeQueue : BaseActivity {

    public override List<string> Execution() {
        base.Execution();
        List<string> nexts = GetNextsFromInteractionConfigurations();
        return [nexts[0]];
    }
}