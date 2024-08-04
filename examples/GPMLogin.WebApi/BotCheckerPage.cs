using System.Text.Json;
using PuppeteerAot;

namespace GPMLogin.WebApi;

public class BotCheckerPage(IPage page)
{
    public async Task<JsonElement> Handle()
    {
        await page.GoToAsync("https://botchecker.net/");
        var waitForResponseAsync = await page.WaitForResponseAsync(p => p.Request.Method == HttpMethod.Post);
        var document = await waitForResponseAsync.JsonAsync();
        return document.RootElement;
    }
}