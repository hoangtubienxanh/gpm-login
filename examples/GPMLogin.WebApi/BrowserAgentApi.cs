using System.Text.Json;

using GPMLogin.Apis;
using GPMLogin.Apis.Supplements;
using GPMLogin.Apis.Supplements.Enums;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using PuppeteerAot;

namespace GPMLogin.WebApi;

public static class BrowserAgentApi
{
    public static void MapBrowserAgentApi(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/gpm-login");

        group.MapGet("/fp-collect", CollectFingerprintData);
    }

    private static async Task<Ok<Dictionary<string, JsonElement?>>> CollectFingerprintData(
        [AsParameters] CollectFingerprintDataContext launcherContext, ILogger<CollectFingerprintDataContext> logger)
    {
        var (id, name, _, _, _, _, _, _, _) = await launcherContext.CreateNamedProfile();
        var (_, _, _, address, _) = await launcherContext.Client.StartProfileAsync(id, new StartProfileRequest()
        {
            CommandLineSwitches =
            [
                // Allow pasting
                "--unsafely-disable-devtools-self-xss-warnings",
                // Required for Browser.getBrowserCommandLine
                "--enable-automation",
            ]
        });
        var browser = await launcherContext.EnsureConnected(address);
        var context = browser.DefaultContext;

        var page = await context.NewPageAsync();
        var cdp = await page.CreateCDPSessionAsync();

        var response = await CollectFingerprintDataCore(context, cdp, logger);

        var choice = Random.Shared.Next(0, 1);
        logger.LogInformation("Executing cleanup strategy {choice} for profile name: {name}", choice, name);

        if (choice is 1)
        {
            // Recommended choice
            await launcherContext.StopAndDelete(id);
        }
        else
        {
            await await Task.Factory.StartNew(async () =>
            {
                // context.CloseAsync() results in PuppeteerAot.PuppeteerException: Non-incognito profiles cannot be closed!
                if (!browser.IsClosed) await cdp.SendAsync("Browser.close");
                await launcherContext.Client.DeleteProfileAsync(id, new DeleteProfileRequest(DeleteType.Full));
            }, TaskCreationOptions.LongRunning);
        }

        return TypedResults.Ok(response);
    }

    private static async Task<Dictionary<string, JsonElement?>> CollectFingerprintDataCore(IBrowserContext context,
        ICDPSession cdp, ILogger<CollectFingerprintDataContext> logger)
    {
        // One or more functions used to collect fingerprint information does require the use of `Runtime.enable`  
        var creepJsObject = new CreepJSPage(await context.NewPageAsync()).Handle();
        var deviceBrowserInfoJsObject = new DeviceAndBrowserInfoPage(await context.NewPageAsync()).Handle();
        var botCheckerJsObject = new BotCheckerPage(await context.NewPageAsync()).Handle();
        var commandLineResponse = cdp.SendAsync("Browser.getBrowserCommandLine");
        Task[] aggregatedTasks = [creepJsObject, deviceBrowserInfoJsObject, botCheckerJsObject, commandLineResponse];

        try
        {
            await Task.WhenAll(aggregatedTasks);
        }
        catch (Exception e)
        {
            logger.LogError(0, e, "Error while processing request");
        }

        string[] keys = ["CreepJS", "Device and browser info", "Bot Checker", "Command line switches"];

        var aggregatedResults = aggregatedTasks
            .Select((task, index) =>
            {
                JsonElement? result = task.IsCompletedSuccessfully && task is Task<JsonElement> jsonTask
                    ? jsonTask.Result
                    : null;
                return new { Key = keys[index], Value = result };
            })
            .ToDictionary(o => o.Key, o => o.Value);

        return aggregatedResults;
    }
}

internal readonly struct CollectFingerprintDataContext
{
    [FromServices] public IGPMLoginClient Client { get; init; }
    [FromServices] public TimeProvider TimeProvider { get; init; }
    [FromServices] public IHttpClientFactory HttpClientFactory { get; init; }
    private HttpClient NamedClient => HttpClientFactory.CreateClient("remote-debugging-address");

    private const string Charset = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    private static string GenerateRandomString(int length)
    {
        return string.Create<object?>(length, null,
            static (chars, _) => Random.Shared.GetItems(Charset, chars));
    }

    public async Task<CreateProfileResponse> CreateNamedProfile()
    {
        var response = await Client.CreateProfileAsync(new CreateProfileRequest
        {
            DisplayName = $"{TimeProvider.GetLocalNow():O} - Transient Profile | {GenerateRandomString(6)}"
        });
        return response;
    }

    public async Task<IBrowser> EnsureConnected(string remoteDebuggingAddress)
    {
        var response = await NamedClient.GetFromJsonAsync<JsonElement>($"http://{remoteDebuggingAddress}/json/version");

        if (!response.TryGetProperty("webSocketDebuggerUrl", out var property) ||
            property.GetString() is not { } webSocketDebuggerUrl)
            throw new InvalidOperationException();

        return await Puppeteer.ConnectAsync(new ConnectOptions
        {
            DefaultViewport = null, BrowserWSEndpoint = webSocketDebuggerUrl
        });
    }

    public async Task StopAndDelete(string profileId)
    {
        await Client.StopProfileAsync(profileId);
        await Client.DeleteProfileAsync(profileId, new DeleteProfileRequest(DeleteType.Full));
    }
}