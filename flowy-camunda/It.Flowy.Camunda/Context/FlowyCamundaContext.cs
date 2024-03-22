using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using It.Flowy.Camunda.Models.Core.Modelling;
using It.Flowy.Camunda.Models.Core.Processing;

namespace It.Flowy.Camunda.Context;

/// <summary>
/// context per la connessione al database
/// </summary>
public class FlowyCamundaContext : DbContext {

  public DbSet<Scope>? Scopes { get; set; }
  public DbSet<Draft>? Drafts { get; set; }
  public DbSet<DraftTrack>? DraftTracks { get; set; }
  public DbSet<Process>? Processes { get; set; }
  public DbSet<Instance>? Instances { get; set; }
  public DbSet<InstanceTrack>? InstanceTracks { get; set; }
  public DbSet<InstanceData>? InstanceDatas { get; set; }
  public DbSet<Interaction>? Interactions { get; set; }
  public DbSet<InteractionTrack>? InteractionTracks { get; set; }

  private readonly string? ConnectionString;
  //private readonly string? DefaultSchema;

  public FlowyCamundaContext(IConfiguration config) {
    // get connection string from configuration
    ConnectionString = config.GetConnectionString("FlowyCamunda");
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
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors();

    //SQLSERVER config context
    //options.UseSqlServer(Configuration.GetConnectionString("Arco"));
  }

}