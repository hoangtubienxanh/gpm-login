using System.Net.Http.Json;
using GPMLogin.Apis;
using GPMLogin.Apis.Supplements;

namespace GPMLogin;

/// <inheritdoc cref="IGPMLoginClient" />
internal sealed class GPMLoginClient(HttpClient http, GPMLoginOptions options)
    : IGPMLoginClient
{
    /// <inheritdoc />
    public async Task<List<Group>> GetGroupsAsync()
    {
        return (await http.GetFromJsonAsync("api/v3/groups",
            GPMLoginSerializerContext.Default.ListGroup))!;
    }

    /// <inheritdoc />
    public async Task<Profile> GetProfileAsync(string profileId)
    {
        return (await http.GetFromJsonAsync($"api/v3/profiles/{profileId}",
            GPMLoginSerializerContext.Default.Profile))!;
    }

    /// <inheritdoc />
    public async Task<ResponseObjectRoot> StopProfileAsync(string profileId)
    {
        return (await http.GetFromJsonAsync(
            $"api/v3/profiles/close/{profileId}",
            GPMLoginSerializerContext.Default.ResponseObjectRoot))!;
    }

    /// <inheritdoc />
    public async Task<List<Profile>> GetProfilesAsync(GetProfilesRequest? request)
    {
        var queryString = request is not null ? $"?{request.ToApiGetProfileParameter()}" : string.Empty;
        return (await http.GetFromJsonAsync(
            $"api/v3/profiles?{queryString}",
            GPMLoginSerializerContext.Default.ListProfile))!;
    }

    /// <inheritdoc />
    public async Task<CreateProfileResponse> CreateProfileAsync(CreateProfileRequest request)
    {
        return await CreateProfileAsyncCore(request.ToApiCreateProfileBody());
    }

    /// <inheritdoc />
    public async Task<ResponseObjectRoot> ModifyProfileAsync(string profileId, ModifyProfileBody request)
    {
        var response = await http.PostAsJsonAsync($"api/v3/profiles/update/{profileId}",
            request,
            GPMLoginSerializerContext.Default.ModifyProfileBody);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync(GPMLoginSerializerContext.Default
            .ResponseObjectRoot))!;
    }

    /// <remarks>This method does not throw an exception if the item does not exist.</remarks>
    /// <inheritdoc />
    public async Task<ResponseObjectRoot> DeleteProfileAsync(string profileId, DeleteProfileRequest request)
    {
        var queryString = $"?{request.ToDeleteProfileParameter()}";
        return (await http.GetFromJsonAsync($"api/v3/profiles/delete/{profileId}{queryString}",
            GPMLoginSerializerContext.Default.ResponseObjectRoot))!;
    }

    /// <inheritdoc />
    public async Task<StartProfileResponse> StartProfileAsync(string profileId, StartProfileRequest? request)
    {
        var queryString = request is not null ? $"?{request.ToApiStartProfileParameter()}" : string.Empty;
        return (await http.GetFromJsonAsync(
            $"api/v3/profiles/start/{profileId}{queryString}",
            GPMLoginSerializerContext.Default.StartProfileResponse))!;
    }

    private async Task<CreateProfileResponse> CreateProfileAsyncCore(CreateProfileBody data)
    {
        var response = await http.PostAsJsonAsync("api/v3/profiles/create",
            data,
            GPMLoginSerializerContext.Default.CreateProfileBody);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync(GPMLoginSerializerContext.Default
            .CreateProfileResponse))!;
    }
}