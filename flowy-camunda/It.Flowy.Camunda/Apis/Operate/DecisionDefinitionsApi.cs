using It.Flowy.Camunda.Models.Operate;

namespace It.Flowy.Camunda.Apis.Operate;

public interface IDecisionDefinitionsService {
  Results<DecisionDefinition>? GetDecisionDefinitions(Quary<DecisionDefinition>? quary = null);
  DecisionDefinition? GetDecisionDefinitionByKey(long key);
}

public class DecisionDefinitionsService : OperateApi, IDecisionDefinitionsService {

  public DecisionDefinitionsService(IAuthApi ias) : base(ias) {}
  
  public Results<DecisionDefinition>? GetDecisionDefinitions(Quary<DecisionDefinition>? quary = null){
    return Post<Results<DecisionDefinition>>(
      GetCompleteUrl("/decision-definitions/search"),
      quary != null ? quary : new {}
    );
  }

  public DecisionDefinition? GetDecisionDefinitionByKey(long key) {
    return Get<DecisionDefinition>(GetCompleteUrl("/decision-definitions/" + key));
  }

}
