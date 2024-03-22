using It.Flowy.Camunda.Models.Tasklist;

namespace It.Flowy.Camunda.Apis.Tasklist;

public interface IFormsApi {
  Form? GetFormByIdAndProcessDefinition(string formId, string processDefinitionKey);
}

public class FormsApi : TasklistApi, IFormsApi {
  
  public FormsApi(IAuthApi ias) : base(ias) { }

  public Form? GetFormByIdAndProcessDefinition(string formId, string processDefinitionKey){
    return Get<Form>(GetCompleteUrl("/forms/" + formId + "?processDefinitionKey="+processDefinitionKey));
  }
}