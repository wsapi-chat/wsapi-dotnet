using WSApi.Client;
using WSAPI.Client.Examples.SSE;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddSimpleConsole(o =>
{
    o.TimestampFormat = "[HH:mm:ss.fff] ";
    o.SingleLine = true;
});

//Configure WSAPI Client
var apiKey = builder.Configuration["WSAPI:ApiKey"] ?? throw new InvalidOperationException("WSApi:ApiKey is not set in configuration");
var instanceId = builder.Configuration["WSAPI:InstanceId"] ?? throw new InvalidOperationException("WSApi:InstanceId is not set in configuration");
builder.Services.AddWsApiClient(apiKey,  instanceId);
//End WSAPI Config

//Add SSE Client Service
builder.Services.AddHostedService<SSEClientService>();

var app = builder.Build();
app.Run();
