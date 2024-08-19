// See https://aka.ms/new-console-template for more information


using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureHostConfiguration(builder =>
    {
        builder.AddJsonFile("appsettings.json");
    })
    .ConfigureServices(builder =>
    {
        builder.AddHostedService<MyService>();
    })
    .Build();


await host.StartAsync();

await host.WaitForShutdownAsync();

public class MyService : BackgroundService
{
    private readonly IHostEnvironment _hostEnvironment;
    private readonly ILogger<MyService> _logger;
    private readonly IConfiguration _configuration;

    public MyService(IHostEnvironment hostEnvironment, ILogger<MyService> logger,IConfiguration configuration)
    {
        _hostEnvironment = hostEnvironment;
        _logger = logger;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Run(() =>
        {
            _logger.LogInformation($"app name: {_hostEnvironment.ApplicationName}");
            _logger.LogInformation($"env name: {_hostEnvironment.EnvironmentName}");
            _logger.LogInformation($"ContentRootPath: {_hostEnvironment.ContentRootPath}");
            _logger.LogInformation($"config: {_configuration["Logging:LogLevel:Microsoft"]}");
            _logger.LogInformation($"config: {_configuration.GetValue<string>("Logging:LogLevel:Microsoft")}");
        });
    }
}