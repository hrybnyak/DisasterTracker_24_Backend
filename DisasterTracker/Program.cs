using DisasterTracker.BL;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using System.Net;

const string corsPolicy = "allow-all";
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("Main class initialization");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    builder.WebHost.ConfigureKestrel(o => o.Listen(IPAddress.Any, Convert.ToInt32(Environment.GetEnvironmentVariable("PORT"))));

    builder.Services.AddCors(sa => sa.AddPolicy(corsPolicy, policy => policy.AllowAnyOrigin()));
    builder.Services.AddBusinessLogicServices(builder.Configuration);
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "DisasterTracker API",
            Description = "A Web API for disaster tracking using open meteorological data.",
            Contact = new OpenApiContact
            {
                Name = "DisasterTracker Email",
                Url = new Uri("mailto: disastertracker24@gmail.com")
            }
        });
    });

    var app = builder.Build();
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();

    app.UseCors(corsPolicy);
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "The program has stopped because of the exception");
    throw;
}