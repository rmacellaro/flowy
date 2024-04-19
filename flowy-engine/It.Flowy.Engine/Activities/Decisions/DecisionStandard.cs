namespace It.Flowy.Engine.Activities.Decisions;

public class DecisionStandard : BaseActivity {

    public override List<string> Execution() {
        base.Execution();
        return ["OUT_1"];
    }
}