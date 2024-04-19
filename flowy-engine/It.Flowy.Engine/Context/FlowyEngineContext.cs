using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace It.Flowy.Engine.Context;

public class FlowyEngineContext : DbContext {

  #region Modelling
  public DbSet<Models.Modelling.Process>? Processes { get; set; }
  public DbSet<Models.Modelling.Distribution>? Distributions { get; set; }
  public DbSet<Models.Modelling.Node>? Nodes { get; set; }
  public DbSet<Models.Modelling.NodeData>? NodeDatas { get; set; }
  public DbSet<Models.Modelling.NodeDataType>? NodeDataTypes { get; set; }
  public DbSet<Models.Modelling.Activity>? Activities { get; set; }
  public DbSet<Models.Modelling.ActivityData>? ActivityDatas { get; set; }
  public DbSet<Models.Modelling.ActivityDefinition>? ActivityDefinitions { get; set; }
  public DbSet<Models.Modelling.ActivityDefinitionDataType>? ActivityDefinitionDataTypes { get; set; }
  public DbSet<Models.Modelling.Link>? Links { get; set; }
  #endregion

  #region Processing
  public DbSet<Models.Processing.InstanceData>? InstanceDatas { get; set; }
  public DbSet<Models.Processing.Instance>? Instances { get; set; }
  public DbSet<Models.Processing.Track>? Tracks { get; set; }
  public DbSet<Models.Processing.Wire>? Wires { get; set; }
  #endregion

  private readonly string? ConnectionString;
  //private readonly string? DefaultSchema;

  public FlowyEngineContext(IConfiguration config) {
    // get connection string from configuration
    ConnectionString = config.GetConnectionString("FlowyEngine");
    //DefaultSchema = "Stateman";
    if (ConnectionString == null) { throw new Exception("No Flowy connection string in config file"); }
  }

  protected override void OnConfiguring(DbContextOptionsBuilder options) {
    // MYSQL config context 
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
    options.UseMySql(ConnectionString, serverVersion, o =>
    {
      o.SchemaBehavior(MySqlSchemaBehavior.Translate, (schema, table) => $"{schema}_{table}");
      //o.MigrationsHistoryTable(DefaultSchema + "__EF_Migrations");
    })
    //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors();

    //SQLSERVER config context
    //options.UseSqlServer(Configuration.GetConnectionString("Arco"));
  }

  /*protected override void OnModelCreating(ModelBuilder modelBuilder) {
    modelBuilder.HasDefaultSchema(DefaultSchema);
  }*/
}