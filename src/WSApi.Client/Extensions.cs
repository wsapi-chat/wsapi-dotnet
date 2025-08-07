using System;
using Microsoft.Extensions.DependencyInjection;
using WSApi.Client.ApiClient;
using WSApi.Client.SSE;

namespace WSApi.Client;

public static class Extensions
{
 
    public static IServiceCollection AddWsApiClient(this IServiceCollection services, string apiKey, string instanceId)
    {
        services.AddHttpClient<IWSApiClient, WSApiClient>((_, http) =>
        {
            http.BaseAddress = new Uri("https://api.wsapi.chat");
            http.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
            http.DefaultRequestHeaders.Add("X-Instance-Id", instanceId);
        });
        
        services.AddHttpClient<ISSEClient, SSEClient>((_, http) =>
        {
            http.BaseAddress = new Uri("https://ws.wsapi.chat");
            http.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
            http.DefaultRequestHeaders.Add("X-Instance-Id", instanceId);
        });
        return services;
    }
}