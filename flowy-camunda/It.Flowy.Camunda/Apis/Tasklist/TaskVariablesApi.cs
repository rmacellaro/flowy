using It.Flowy.Camunda.Models.Tasklist;

namespace It.Flowy.Camunda.Apis.Tasklist;

public interface ITaskVariablesApi {
  TaskVariable? GetVariableById(string variableId);
}

public class TaskVariablesService : TasklistApi, ITaskVariablesApi {
  public TaskVariablesService(IAuthApi ias) : base(ias) { }

  public TaskVariable? GetVariableById(string variableId){
    return Get<TaskVariable>(GetCompleteUrl("/variables/" + variableId));
  }
}