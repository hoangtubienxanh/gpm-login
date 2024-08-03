namespace GPMLogin.Apis;

/// <summary>
///     Represents the response returned when starting a profile.
/// </summary>
/// <param name="Success">Indicates whether the profile was started successfully.</param>
/// <param name="ProfileId">The current profile Id.</param>
/// <param name="BrowserLocation">The location of the browser.</param>
/// <param name="RemoteDebuggingAddress">The address for remote debugging.</param>
/// <param name="DriverPath">The path to the driver.</param>
public sealed record StartProfileResponse(
    bool Success,
    string ProfileId,
    string BrowserLocation,
    string RemoteDebuggingAddress,
    string DriverPath
);