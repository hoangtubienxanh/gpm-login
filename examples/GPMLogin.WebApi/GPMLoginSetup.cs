using System.Text.Json;

using GPMLogin.Apis;
using GPMLogin.Apis.Supplements;
using GPMLogin.Apis.Supplements.Enums;


namespace GPMLogin.WebApi;

public class GPMLoginSetup(IGPMLoginClient client, IHttpClientFactory httpClientFactory, TimeProvider timeProvider)
{
    private HttpClient NamedClient => httpClientFactory.CreateClient("remote-debugging-address");

    private const string Charset = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    private static string GenerateRandomString(int length)
    {
        return string.Create<object?>(length, null,
            static (chars, _) => Random.Shared.GetItems(Charset, chars));
    }

    public async Task<CreateProfileResponse> CreateNamedProfile()
    {
        var response = await client.CreateProfileAsync(new CreateProfileRequest
        {
            DisplayName = $"{timeProvider.GetLocalNow():O} - Transient Profile | {GenerateRandomString(6)}"
        });
        return response;
    }

    public async Task<string> GetBrowserWSConnection(string remoteDebuggingAddress)
    {
        var response = await NamedClient.GetFromJsonAsync<JsonElement>($"http://{remoteDebuggingAddress}/json/version");

        if (!response.TryGetProperty("webSocketDebuggerUrl", out var property) ||
            property.GetString() is not { } webSocketDebuggerUrl)
            throw new InvalidOperationException();

        return webSocketDebuggerUrl;
    }
    
    public async Task StopAndDelete(string profileId)
    {
        await client.StopProfileAsync(profileId);
        await client.DeleteProfileAsync(profileId, new DeleteProfileRequest(DeleteType.Full));
    }
}