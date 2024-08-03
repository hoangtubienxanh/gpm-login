namespace GPMLogin;

/// <summary>
///     A problem details contract as defined in RFC 7807.
/// </summary>
public record GPMLoginProblemDetails(
    string? Type,
    string Title,
    int Status,
    string? Detail,
    string? Instance);