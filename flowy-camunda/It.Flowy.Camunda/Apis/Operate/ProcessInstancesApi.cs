using It.Flowy.Camunda.Models.Operate;

namespace It.Flowy.Camunda.Apis.Operate;

public interface IProcessInstancesApi {
  Results<ProcessInstance>? GetProcessInstances(Quary<ProcessInstance>? quary = null);
  ProcessInstance? GetProcessInstanceByKey(long key);
  /// <summary>
  /// Delete process instance and all dependent data key
  /// </summary>
  /// <param name="key"></param>
  ChangeStatus? DeleteProcessInstanceByKey(long key);
  List<FlowNodeStatistics>? GetProcessInstanceStatistics(long key);
  List<FlowNodeStatistics>? GetProcessInstancesStatisticsByProcessDefinition(long keyProcessDefinition);
  List<string>? GetProcessInstanceSequenceFlows(long key);
}

public class ProcessInstancesApi : OperateApi, IProcessInstancesApi {

  public ProcessInstancesApi(IAuthApi ias) : base(ias) {}
  
  public Results<ProcessInstance>? GetProcessInstances(Quary<ProcessInstance>? quary = null){
    return Post<Results<ProcessInstance>>(
      GetCompleteUrl("/process-instances/search"),
      quary != null ? quary : new {}
    );
  }

  public ProcessInstance? GetProcessInstanceByKey(long key) {
    return Get<ProcessInstance>(GetCompleteUrl("/process-instances/" + key));
  }

  public ChangeStatus? DeleteProcessInstanceByKey(long key){
    return Delete<ChangeStatus>(GetCompleteUrl("/process-instances/" + key));
  }
  
  public List<FlowNodeStatistics>? GetProcessInstanceStatistics(long key){
    return Get<List<FlowNodeStatistics>>(GetCompleteUrl("/process-instances/" + key + "/statistics"));
  }

  public List<FlowNodeStatistics>? GetProcessInstancesStatisticsByProcessDefinition(long keyProcessDefinition){
    return Post<List<FlowNodeStatistics>>(GetCompleteUrl("/process-instances/statistics").Replace("v1","api"), new {
      active = true,
      incidents = true,
      running = true,
      processIds = new List<string>() { keyProcessDefinition.ToString() }
    });
  }

  public List<string>? GetProcessInstanceSequenceFlows(long key){
    return Get<List<string>>(GetCompleteUrl("/process-instances/" + key + "/sequence-flows"));
  }
}
