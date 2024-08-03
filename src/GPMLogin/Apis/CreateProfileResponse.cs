namespace GPMLogin.Apis;

/// <summary>
///     Represents the response returned when creating a profile.
/// </summary>
/// <param name="Id">The ID of the profile.</param>
/// <param name="Name">The name of the profile.</param>
/// <param name="RawProxy">The raw proxy settings of the profile.</param>
/// <param name="ProfilePath">The file path to the profile.</param>
/// <param name="BrowserType">The type of browser used by the profile.</param>
/// <param name="BrowserVersion">The version of the browser used by the profile.</param>
/// <param name="Note">Additional notes associated with the profile.</param>
/// <param name="GroupId">The ID of the group to which the profile belongs.</param>
/// <param name="CreatedAt">The date and time when the profile was created, formatted as <c>yyyy-MM-ddTHH:mm:ss</c>.</param>
public sealed record CreateProfileResponse(
    string Id,
    string Name,
    string RawProxy,
    string ProfilePath,
    string BrowserType,
    string BrowserVersion,
    object Note,
    int GroupId,
    string CreatedAt);