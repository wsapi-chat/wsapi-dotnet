using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WSApi.Client.ApiClient;

public static class HttpClientExtensions
{
    public static async Task<ApiResponse<T>> ReadAsApiResponseJsonAsync<T>(this HttpResponseMessage response)
    {
        try
        {
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<T>();
                return new ApiResponse<T> { Result = result };
            }

            var error = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            return new ApiResponse<T> { Error = error };
        }
        catch (Exception ex)
        {
            return HandleException<T>(ex);
        }
    }
    
    public static async Task<ApiResponse<byte[]>> ReadAsApiResponseByteArrayAsync(this HttpResponseMessage response)
    {
        try
        {
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsByteArrayAsync();
                return new ApiResponse<byte[]> { Result = result };
            }

            var error = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            return new ApiResponse<byte[]> { Error = error };
        }
        catch (Exception ex)
        {
            return HandleException<byte[]>(ex);
        }
    }
    
    public static async Task<ApiResponse> ReadAsApiResponseAsync(this HttpResponseMessage response)
    {
        try
        {
            if (response.IsSuccessStatusCode)
                return new ApiResponse();

            var error = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            return new ApiResponse { Error = error };
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    
    public static async Task<T> EnsureSuccessOrThrowJsonAsync<T>(this HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<T>() ?? throw new InvalidOperationException("Empty response");

        await response.ThrowApiExceptionAsync();
        return default!;
    }
    
    public static async Task<byte[]> EnsureSuccessOrThrowByteArrayAsync(this HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsByteArrayAsync() ?? throw new InvalidOperationException("Empty response");

        await response.ThrowApiExceptionAsync();
        return null!;
    }
    
    public static async Task EnsureSuccessOrThrowAsync(this HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return;

        await response.ThrowApiExceptionAsync();
    }
    
    
    private static ApiResponse<T> HandleException<T>(Exception ex)
    {
        return ex switch
        {
            TaskCanceledException tcEx when tcEx.InnerException is TimeoutException => new ApiResponse<T>
            {
                Error = new ProblemDetails
                {
                    Status = 408,
                    Title = "Request Timeout",
                    Detail = "The request timed out"
                }
            },
            HttpRequestException httpEx => new ApiResponse<T>
            {
                Error = new ProblemDetails
                {
                    Status = 500,
                    Title = "Request Failed",
                    Detail = httpEx.Message
                }
            },
            _ => new ApiResponse<T>
            {
                Error = new ProblemDetails
                {
                    Status = 500,
                    Title = "Request Failed",
                    Detail = ex.Message
                }
            }
        };
    }
    
    private static ApiResponse HandleException(Exception ex)
    {
        return ex switch
        {
            TaskCanceledException tcEx when tcEx.InnerException is TimeoutException => new ApiResponse
            {
                Error = new ProblemDetails
                {
                    Status = 408,
                    Title = "Request Timeout",
                    Detail = "The request timed out"
                }
            },
            _ => new ApiResponse
            {
                Error = new ProblemDetails
                {
                    Status = 500,
                    Title = "Request Failed",
                    Detail = ex.Message
                }
            }
        };
    }
    
    private static async Task ThrowApiExceptionAsync(this HttpResponseMessage response)
    {
        // Try to read as ProblemDetails first
        ProblemDetails? error = null;
        try
        {
            error = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        }
        catch
        {
            // ignore
        }
        if (error != null)
            throw new ApiException(error);

        var content = await response.Content.ReadAsStringAsync();
        throw new ApiException(new ProblemDetails { Status = (int)response.StatusCode, Detail = content });
    }
}