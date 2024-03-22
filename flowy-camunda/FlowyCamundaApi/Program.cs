using log4net;
using log4net.Config;
using log4net.Repository;
using System.Reflection;
using It.Flowy.Camunda;

ILoggerRepository logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddNewtonsoftJson(x =>
    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddFlowyCamundaConfig();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => {
    options.AddPolicy("default", policy => {
        policy.SetIsOriginAllowed(_ => true)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .Build();
    });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("default");
app.MapControllers();
app.Run();
