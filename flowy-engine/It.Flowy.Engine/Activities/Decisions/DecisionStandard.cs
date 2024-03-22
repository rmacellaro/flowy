namespace It.Flowy.Engine.Activities.Decisions;

public class DecisionStandard : BaseActivity {

    public override List<string> Execution() {
        base.Execution();
        List<string> nexts = GetNextsFromInteractionConfigurations();
        return [nexts[0]];
    }
}