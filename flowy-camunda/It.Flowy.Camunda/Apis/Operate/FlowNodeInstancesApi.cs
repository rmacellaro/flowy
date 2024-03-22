using It.Flowy.Camunda.Models.Operate;

namespace It.Flowy.Camunda.Apis.Operate;

public interface IFlowNodeInstancesApi {
  Results<FlowNodeInstance>? GetFlowNodeInstances(Quary<FlowNodeInstance>? quary = null);
  FlowNodeInstance? GetFlowNodeInstanceByKey(long key);
}

public class FlowNodeInstancesApi : OperateApi, IFlowNodeInstancesApi {

  public FlowNodeInstancesApi(IAuthApi ias) : base(ias) {}
  
  public Results<FlowNodeInstance>? GetFlowNodeInstances(Quary<FlowNodeInstance>? quary = null){
    return Post<Results<FlowNodeInstance>>(
      GetCompleteUrl("/flownode-instances/search"),
      quary != null ? quary : new {}
    );
  }

  public FlowNodeInstance? GetFlowNodeInstanceByKey(long key) {
    return Get<FlowNodeInstance>(GetCompleteUrl("/flownode-instances/" + key));
  }

}
