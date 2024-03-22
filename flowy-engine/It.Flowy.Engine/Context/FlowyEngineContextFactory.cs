using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace It.Flowy.Engine.Context;

internal class FlowyEngineContextFactory : IDesignTimeDbContextFactory<FlowyEngineContext> {

    public FlowyEngineContext CreateDbContext(string[] args) {
        string fullPath = Path.GetFullPath(@"../FlowyEngineApi/appsettings.json");
        IConfiguration config = new ConfigurationBuilder().AddJsonFile(fullPath).Build();
        return new FlowyEngineContext(config);
    }
}