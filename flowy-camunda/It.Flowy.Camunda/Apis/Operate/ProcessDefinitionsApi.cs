using It.Flowy.Camunda.Models.Operate;

namespace It.Flowy.Camunda.Apis.Operate;

public interface IProcessDefinitionsApi {
  Results<ProcessDefinition>? GetProcessDefinitions(Quary<ProcessDefinition>? quary = null);
  ProcessDefinition? GetProcessDefinitionByKey(long key);
  string? GetProcessDefinitionSchemaByKey(long key);
}

public class ProcessDefinitionsApi : OperateApi, IProcessDefinitionsApi {
  public ProcessDefinitionsApi(IAuthApi ias) : base(ias){ }

  public Results<ProcessDefinition>? GetProcessDefinitions(Quary<ProcessDefinition>? quary = null) {
    return Post<Results<ProcessDefinition>>(
      GetCompleteUrl("/process-definitions/search"),
      quary != null ? quary : new {}
    );
  }

  public ProcessDefinition? GetProcessDefinitionByKey(long key) {
    return Get<ProcessDefinition>(GetCompleteUrl("/process-definitions/" + key));
  }

  public string? GetProcessDefinitionSchemaByKey(long key){
    return Get<string>(GetCompleteUrl("/process-definitions/" + key + "/xml"));
  }
}
