using System.Text.Json;
using GPMLogin.Apis;
using GPMLogin.Apis.Supplements;
using GPMLogin.Apis.Supplements.Enums;
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

    private static async Task<object> CollectFingerprintData(
        [AsParameters] CollectFingerprintDataContext launcherContext, ILogger<CollectFingerprintDataContext> logger)
    {
        var (id, name, _, _, _, _, _, _, _) = await launcherContext.CreateNamedProfile();
        var (_, _, _, address, _) = await launcherContext.Client.StartProfileAsync(id);
        var browser = await launcherContext.EnsureConnected(address);
        var context = browser.DefaultContext;
        
        // One or more functions used to collect fingerprint information does require the use of `Runtime.enable`  
        var creepJsObject = new CreepJSPage(await context.NewPageAsync()).Handle();
        var deviceBrowserInfoJsObject = new DeviceAndBrowserInfoPage(await context.NewPageAsync()).Handle();
        var botCheckerJsObject = new BotCheckerPage(await context.NewPageAsync()).Handle();

        var task = Task.WhenAll(creepJsObject, deviceBrowserInfoJsObject, botCheckerJsObject);

        try
        {
            await task;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Catching {e.GetType().FullName} {e is AggregateException}");
            Console.WriteLine($"Inspecting {task.Exception?.GetType().FullName} {task.Exception is not null}");
        }

        var response = new Dictionary<string, object>
        {
            { "CreepJS", creepJsObject.Result.HasValue ? creepJsObject.Result.Value : "<timeout>" },
            {
                "Device and browser info",
                deviceBrowserInfoJsObject.Result.HasValue ? deviceBrowserInfoJsObject.Result.Value : "<timeout>"
            },
            { "Bot Checker", botCheckerJsObject.Result.HasValue ? botCheckerJsObject.Result.Value : "<timeout>" }
        };

        var choice = Random.Shared.Next(0, 1);
        logger.LogInformation("Executing cleanup strategy {choice} for profile name: {name}", choice, name);

        if (choice is 1)
        {
            // Recommended choice
            await launcherContext.Client.StopProfileAsync(id);
            await launcherContext.Client.DeleteProfileAsync(id, new DeleteProfileRequest(DeleteType.Full));
        }
        else
        {
            var page = await context.NewPageAsync();
            var cdp = await page.CreateCDPSessionAsync();
            await await Task.Factory.StartNew(async () =>
            {
                // context.CloseAsync() results in PuppeteerAot.PuppeteerException: Non-incognito profiles cannot be closed!
                if (!browser.IsClosed) await cdp.SendAsync("Browser.close");

                await launcherContext.Client.DeleteProfileAsync(id, new DeleteProfileRequest(DeleteType.Full));
            }, TaskCreationOptions.LongRunning);
        }

        return response;
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
}