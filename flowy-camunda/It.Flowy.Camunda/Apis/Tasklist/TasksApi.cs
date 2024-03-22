using It.Flowy.Camunda.Models.Tasklist;
using Task = It.Flowy.Camunda.Models.Tasklist.Task;

namespace It.Flowy.Camunda.Apis.Tasklist;

public interface ITasksApi {

  /// <summary>
  /// This operation validates the task and draft variables, deletes existing draft variables for the task, and then checks for new draft variables. 
  /// If a new variable's name matches an existing one but the value differs, it is saved. In case of duplicate draft variable names, the last variable's value is kept.
  /// </summary>
  /// <param name="taskId"></param>
  /// <param name="variables"></param>
  /// <returns></returns>
  bool SetTaskVariables(string taskId, Dictionary<string,string> variables);

  /// <summary>
  /// This method returns a list of task variables for the specified taskId and variableName.
  /// If the request body is not provided or if the variableNames parameter in the request is empty, 
  /// all variables associated with the task will be returned.
  /// </summary>
  /// <param name="taskId"></param>
  /// <param name="variableNames"></param>
  /// <returns></returns>
  List<TaskVariable>? GetTaskVariables(string taskId, List<string> variableNames);

  /// <summary>
  /// Returns the list of tasks that satisfy search request params.
  /// NOTE: Only one of [searchAfter, searchAfterOrEqual, searchBefore, searchBeforeOrEqual]search options must be present in request.
  /// </summary>
  /// <param name="quary"></param>
  /// <returns></returns>
  List<Task>? GetTasks(TaskQuary? quary = null);

  /// <summary>
  /// Unassign a task with taskId.
  /// </summary>
  /// <param name="taskId"></param>
  /// <returns></returns>
  Task? SetTaskUnassign(string taskId);

  /// <summary>
  /// Complete a task with taskId and optional variables
  /// </summary>
  /// <param name="taskId"></param>
  /// <param name="variables"></param>
  /// <returns></returns>
  Task? SetTaskComplete(string taskId, Dictionary<string,string> variables);

  /// <summary>
  /// Assign a task with taskId to assignee or the active user.
  /// </summary>
  /// <param name="taskId">taskid to assign</param>
  /// <param name="assignee">When using a JWT token, the assignee parameter is NOT optional when called directly from the API. The system will not be able to detect the assignee from the JWT token, therefore the assignee parameter needs to be explicitly passed in this instance.</param>
  /// <param name="allowOverrideAssignment">When true the task that is already assigned may be assigned again. Otherwise the task must be first unassign and only then assign again. (Default: true)</param>
  /// <returns>task</returns>
  Task? SetTaskAssign(string taskId, string assignee, bool allowOverrideAssignment);
  
  /// <summary>
  /// Get one task by id. Returns task or error when task does not exist.
  /// </summary>
  /// <param name="taskId"></param>
  /// <returns></returns>
  Task? GetTaskById(string taskId);
  
}

public class TasksApi : TasklistApi, ITasksApi {
  
  public TasksApi(IAuthApi ias) : base(ias) { }

  public bool SetTaskVariables(string taskId, Dictionary<string,string> variables) {
    string? r = Post<string>(GetCompleteUrl("/tasks/" + taskId + "/variables"), new {
      variables
    });
    return true;
  }

  public List<TaskVariable>? GetTaskVariables(string taskId, List<string> variableNames){
    return Post<List<TaskVariable>>(GetCompleteUrl("/tasks/" + taskId + "/variables/search"), new {
      variableNames
    });
  }

  public List<Task>? GetTasks(TaskQuary? quary = null) {
    return Post<List<Task>>(
      GetCompleteUrl("/tasks/search"), 
      quary != null ? quary : new {}
    );
  }

  public Task? SetTaskUnassign(string taskId) {
    return Patch<Task>(GetCompleteUrl("/tasks/" + taskId + "/unassign"));
  }

  public Task? SetTaskComplete(string taskId, Dictionary<string,string> variables){
    return Patch<Task>(GetCompleteUrl("/tasks/" + taskId + "/complete"), new {
      variables
    });
  }

  public Task? SetTaskAssign(string taskId, string assignee, bool allowOverrideAssignment){
    return Patch<Task>(GetCompleteUrl("/tasks/" + taskId + "/assign"), new {
      assignee,
      allowOverrideAssignment
    });
  }

  public Task? GetTaskById(string taskId) {
    return Get<Task>(GetCompleteUrl("/tasks/" + taskId));
  }
}