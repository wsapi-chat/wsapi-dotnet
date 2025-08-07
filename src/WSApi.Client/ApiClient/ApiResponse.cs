namespace WSApi.Client.ApiClient;

public class ApiResponse<T>
{
    public T? Result { get; init; }
    public ProblemDetails? Error { get; init; }
    public bool IsSuccess => Error is null;
}

public class ApiResponse
{
    public ProblemDetails? Error { get; init; }
    public bool IsSuccess => Error is null;
}