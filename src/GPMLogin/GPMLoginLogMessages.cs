using Microsoft.Extensions.Logging;

namespace GPMLogin;

internal static partial class GPMLoginLogMessages
{
    [LoggerMessage(EventId = 1, Level = LogLevel.Information,
        Message = "Start intercepting HTTP response {RequestUri}")]
    public static partial void LogResponseInterceptionStarted(this ILogger logger, string? requestUri);

    [LoggerMessage(EventId = 2, Level = LogLevel.Critical, Message = """
                                                                     FATAL ERROR: Failed to deserialize the response body content
                                                                     Please report this issue on GitHub.
                                                                     """)]
    public static partial void LogUnhandledException(this ILogger logger);
}