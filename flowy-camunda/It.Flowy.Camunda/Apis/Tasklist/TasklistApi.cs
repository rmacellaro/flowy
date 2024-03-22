
namespace It.Flowy.Camunda.Apis.Tasklist;

public class TasklistApi : BaseApi {

  public TasklistApi(IAuthApi ias) : base(ias) {
    UrlApi = "http://localhost:8082/v1";
  }

}