using System;

namespace WSApi.Client.ApiClient;

public class ApiException(ProblemDetails problem) : Exception(problem.Detail ?? problem.Title)
{
    public ProblemDetails Problem { get; } = problem;
}