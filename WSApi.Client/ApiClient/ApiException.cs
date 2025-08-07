using System;
using Microsoft.AspNetCore.Mvc;

namespace WSApi.Client.ApiClient;

public class ApiException(ProblemDetails problem) : Exception(problem.Detail ?? problem.Title)
{
    public ProblemDetails Problem { get; } = problem;
}