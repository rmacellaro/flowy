using It.Flowy.Camunda.Models.Operate;

namespace It.Flowy.Camunda.Apis.Operate;

public interface IVariablesApi {
  Results<Variable>? GetVariables(Quary<Variable>? quary = null);
  Variable? GetVariableByKey(long key);
}

public class VariablesApi : OperateApi, IVariablesApi {

  public VariablesApi(IAuthApi ias) : base(ias) {}
  
  public Results<Variable>? GetVariables(Quary<Variable>? quary = null){
    return Post<Results<Variable>>(
      GetCompleteUrl("/variables/search"),
      quary != null ? quary : new {}
    );
  }

  public Variable? GetVariableByKey(long key) {
    return Get<Variable>(GetCompleteUrl("/variables/" + key));
  }

}
