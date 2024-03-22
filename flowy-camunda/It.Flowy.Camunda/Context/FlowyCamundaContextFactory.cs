using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace It.Flowy.Camunda.Context;

internal class FlowyEngineContextFactory : IDesignTimeDbContextFactory<FlowyCamundaContext> {
    public FlowyCamundaContext CreateDbContext(string[] args) {
        string fullPath = Path.GetFullPath(@"../FlowyCamundaApi/appsettings.json");
        IConfiguration config = new ConfigurationBuilder().AddJsonFile(fullPath).Build();
        return new FlowyCamundaContext(config);
    }
}
