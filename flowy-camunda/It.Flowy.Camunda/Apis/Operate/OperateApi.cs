namespace It.Flowy.Camunda.Apis.Operate;

public class OperateApi : BaseApi {
  public OperateApi(IAuthApi ias) : base(ias) {
    UrlApi = "http://localhost:8081/v1";
  }
}