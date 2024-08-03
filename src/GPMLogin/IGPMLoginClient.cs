using GPMLogin.Apis;
using GPMLogin.Apis.Supplements;

namespace GPMLogin;

/// <summary>
///     Provides APIs that help you interact with GPM-Login API.
/// </summary>
public interface IGPMLoginClient
{
    /// <summary>
    ///     Lists all groups in the app.
    /// </summary>
    Task<List<Group>> GetGroupsAsync();

    /// <summary>
    ///     Lists all profiles in the app, optionally filtered by specified criteria.
    /// </summary>
    Task<List<Profile>> GetProfilesAsync(GetProfilesRequest? request = default);

    /// <summary>
    ///     Gets the details of a specific profile.
    /// </summary>
    Task<Profile> GetProfileAsync(string profileId);

    /// <summary>
    ///     Attempts to delete a profile with the specified ID.
    /// </summary>
    Task<CreateProfileResponse> CreateProfileAsync(CreateProfileRequest request);

    /// <summary>
    ///     Attempts to modify a profile with the specified ID.
    /// </summary>
    Task<ResponseObjectRoot> ModifyProfileAsync(string profileId, ModifyProfileBody request);

    /// <summary>
    ///     Attempts to delete a profile with the specified ID.
    /// </summary>
    Task<ResponseObjectRoot> DeleteProfileAsync(string profileId, DeleteProfileRequest request);

    /// <summary>
    ///     Attempts to start a profile with the specified ID.
    /// </summary>
    Task<StartProfileResponse> StartProfileAsync(string profileId, StartProfileRequest? request = default);

    /// <summary>
    ///     Attempts to stop a profile with the specified ID.
    /// </summary>
    Task<ResponseObjectRoot> StopProfileAsync(string profileId);
}