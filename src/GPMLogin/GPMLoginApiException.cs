namespace GPMLogin;

public sealed class GPMLoginApiException(GPMLoginProblemDetails problemDetails) : Exception(problemDetails.Title)
{
    /// <summary>
    ///     Details associated with the problem.
    /// </summary>
    public GPMLoginProblemDetails Details { get; } = problemDetails;
}