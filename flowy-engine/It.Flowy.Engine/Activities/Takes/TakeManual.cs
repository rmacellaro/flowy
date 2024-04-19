namespace It.Flowy.Engine.Activities.Takes;

public class TakeManual : BaseActivity {

    public override List<string> Execution() {
        base.Execution();
        return ["OUT_1"];
    }
}