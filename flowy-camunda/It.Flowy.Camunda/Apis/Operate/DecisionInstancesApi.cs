using It.Flowy.Camunda.Models.Operate;

namespace It.Flowy.Camunda.Apis.Operate;

public interface IDecisionInstancesService {
  Results<DecisionInstance>? GetDecisionInstances(Quary<DecisionInstance>? quary = null);
  DecisionInstance? GetDecisionInstanceById(string id);
}

public class DecisionInstancesService : OperateApi, IDecisionInstancesService {

  public DecisionInstancesService(IAuthApi ias) : base(ias) {}
  
  public Results<DecisionInstance>? GetDecisionInstances(Quary<DecisionInstance>? quary = null){
    return Post<Results<DecisionInstance>>(
      GetCompleteUrl("/decision-instances/search"),
      quary != null ? quary : new {}
    );
  }

  public DecisionInstance? GetDecisionInstanceById(string id) {
    return Get<DecisionInstance>(GetCompleteUrl("/decision-instances/" + id));
  }

}
