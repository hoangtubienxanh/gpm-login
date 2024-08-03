namespace GPMLogin.Apis;

/// <summary>
///     Represents a browser profile data.
/// </summary>
/// <param name="Id">The current profile Id.</param>
/// <param name="Name">The name of the profile.</param>
/// <param name="RawProxy">The raw proxy settings of the profile, if any.</param>
/// <param name="BrowserType">The type of browser used by the profile.</param>
/// <param name="BrowserVersion">The version of the browser used by the profile.</param>
/// <param name="GroupId">The ID of the group to which the profile belongs.</param>
/// <param name="ProfilePath">The file path to the profile.</param>
/// <param name="Note">Additional notes associated with the profile, if any.</param>
/// <param name="CreatedAt">The date and time when the profile was created, formatted as <c>yyyy-MM-ddTHH:mm:ss</c>.</param>
public sealed record Profile(
    string Id,
    string Name,
    string? RawProxy,
    string BrowserType,
    string BrowserVersion,
    int GroupId,
    string ProfilePath,
    string? Note,
    string CreatedAt);