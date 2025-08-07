using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WSApi.Client;
using WSAPI.Client.Examples.Console;

var host = Host.CreateDefaultBuilder(args)

    //Configuration
    .ConfigureAppConfiguration((ctx, cfg) =>
    {
        cfg.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
    })

    //Logger
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddSimpleConsole(o =>
        {
            o.TimestampFormat = "[HH:mm:ss.fff] ";
            o.SingleLine = true;
        });
    })

    //WSApi with SSE
    .ConfigureServices((ctx, services) =>
    {
        //Configure WSAPI Client
        var apiKey = ctx.Configuration["WSAPI:ApiKey"] ?? throw new InvalidOperationException("WSAPI:ApiKey is not set in configuration");
        var instanceId = ctx.Configuration["WSAPI:InstanceId"] ?? throw new InvalidOperationException("WSAPI:InstanceId is not set in configuration");
        services.AddWsApiClient(apiKey,  instanceId);
        //End WSAPI Config
        
        services.AddHostedService<SSEClientService>();
    })
    .Build();


//Run until CTRL+C / SIGTERM
await host.RunAsync();