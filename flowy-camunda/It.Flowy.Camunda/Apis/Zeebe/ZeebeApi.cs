using System.Text;
using Zeebe.Client;
using Zeebe.Client.Api.Responses;

namespace It.Flowy.Camunda.Apis.Zeebe;

public interface IZeebeApi {
  IZeebeClient GetClient();
  IDeployResourceResponse Deploy(string schema, Encoding? encoding = null, string? resourceName = null );
  IProcessInstanceResponse CreateProcessInstance(long processDefinitionKey, string variables = "");
  ISetVariablesResponse SetVariables(long processInstanceKey, string variables);
  IPublishMessageResponse Publish(string messageName, string correlationKey, string? variables = null);
}

public class ZeebeApi : IZeebeApi {

  private IZeebeClient? CurrentZeebeClient;

  public IZeebeClient GetClient() {
    if (CurrentZeebeClient == null) {
      CurrentZeebeClient = ZeebeClient.Builder()
        .UseGatewayAddress("http://localhost:26500")
        .UsePlainText()
        .Build();
    }
    return CurrentZeebeClient;
  }

  public IDeployResourceResponse Deploy(string schema, Encoding? encoding = null, string? resourceName = null ) {
    encoding ??= Encoding.UTF8;
    resourceName ??= "schema.bpmn";
    return GetClient().NewDeployCommand()
      .AddResourceString(schema, encoding, resourceName)
      .Send().Result;
  }

  public IProcessInstanceResponse CreateProcessInstance(long processDefinitionKey, string variables = "") {
    return GetClient().NewCreateProcessInstanceCommand()
      .ProcessDefinitionKey(processDefinitionKey)
      .Variables(variables)
      .Send().Result;
  }

  public ISetVariablesResponse SetVariables(long processInstanceKey, string variables) {
    return GetClient().NewSetVariablesCommand(processInstanceKey)
      .Variables(variables)
      .Local()
      .Send().Result;
  }

  public IPublishMessageResponse Publish(string messageName, string correlationKey, string? variables = null) {
    return GetClient().NewPublishMessageCommand()
      .MessageName(messageName)
      .CorrelationKey(correlationKey)
      .Variables(variables)
      .Send().Result;
  }
}