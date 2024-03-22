using It.Flowy.Camunda.Apis;
using It.Flowy.Camunda.Apis.Operate;
using It.Flowy.Camunda.Apis.Tasklist;
using It.Flowy.Camunda.Apis.Zeebe;
using It.Flowy.Camunda.Context;
using It.Flowy.Camunda.Services;
using It.Flowy.Camunda.Logic;
using Microsoft.Extensions.DependencyInjection;

namespace It.Flowy.Camunda;

public static class Factory {

  public static IServiceCollection AddFlowyCamundaConfig(this IServiceCollection services) {
    services.AddDbContext<FlowyCamundaContext>();

    // camunda connection
    services.AddScoped<IAuthApi, AuthApi>();
    services.AddScoped<IProcessDefinitionsApi, ProcessDefinitionsApi>();
    services.AddScoped<IProcessInstancesApi, ProcessInstancesApi>();
    services.AddScoped<IZeebeApi, ZeebeApi>();
    services.AddScoped<IVariablesApi, VariablesApi>();
    services.AddScoped<ITasksApi, TasksApi>();
    services.AddScoped<IFlowNodeInstancesApi, FlowNodeInstancesApi>();
    services.AddScoped<IFormsApi, FormsApi>();
    // flowy core
    services.AddScoped<IScopesService, ScopesService>();
    services.AddScoped<IProcessesService, ProcessesService>();
    services.AddScoped<IDraftsService, DraftsService>();
    services.AddScoped<IInstancesService, InstancesService>();
    services.AddScoped<IInteractionsService, InteractionsService>();
    // flowy Logic
    services.AddScoped<IScopesLogic, ScopesLogic>();
    services.AddScoped<IProcessesLogic, ProcessesLogic>();
    services.AddScoped<IDraftsLogic, DraftsLogic>();
    services.AddScoped<IProcessingLogic, ProcessingLogic>();
    services.AddScoped<IInstancesLogic, InstancesLogic>();
    services.AddScoped<IInteractionsLogic, InteractionsLogic>();

    return services;
  }
}