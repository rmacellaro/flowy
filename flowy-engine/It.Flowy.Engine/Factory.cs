using It.Flowy.Engine.Context;
using Microsoft.Extensions.DependencyInjection;

namespace It.Flowy.Engine;

public static class Factory {

  public static IServiceCollection AddFlowyEngineConfig(this IServiceCollection services) {
    services.AddDbContext<FlowyEngineContext>();

    services.AddScoped<Services.Modelling.INodeDatasService, Services.Modelling.NodeDatasService>();
    services.AddScoped<Services.Modelling.INodesService, Services.Modelling.NodesService>();
    services.AddScoped<Services.Modelling.INodeDataTypesService, Services.Modelling.NodeDataTypesService>();
    services.AddScoped<Services.Modelling.IProcessesService, Services.Modelling.ProcessesService>();
    services.AddScoped<Services.Modelling.IDistributionsService, Services.Modelling.DistributionsService>();
    services.AddScoped<Services.Modelling.IActivitiesService, Services.Modelling.ActivitiesService>();
    services.AddScoped<Services.Modelling.IActivityDefinitionsService, Services.Modelling.ActivityDefinitionsService>();

    services.AddScoped<Services.Processing.IDatasService, Services.Processing.DatasService>();
    services.AddScoped<Services.Processing.IInstancesService, Services.Processing.InstancesService>();
    services.AddScoped<Services.Processing.ITracksService, Services.Processing.TracksService>();
    services.AddScoped<Services.Processing.IWiresService, Services.Processing.WiresService>();

    services.AddScoped<Logic.IProcessingLogic, Logic.ProcessingLogic>();
    services.AddScoped<Logic.IModellingLogic, Logic.ModellingLogic>();

    return services;
  }
}