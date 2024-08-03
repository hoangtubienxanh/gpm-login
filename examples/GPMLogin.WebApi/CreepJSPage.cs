using System.Text.Json;
using PuppeteerAot;

namespace GPMLogin.WebApi;

public class CreepJSPage(IPage page)
{
    public async Task<JsonElement?> Handle()
    {
        await page.GoToAsync("https://abrahamjuliot.github.io/creepjs/");
        await page.WaitForExpressionAsync("window.Fingerprint",
            new WaitForFunctionOptions { Timeout = (int)TimeSpan.FromMinutes(1).TotalMilliseconds });
        return await page.EvaluateExpressionAsync("window.Fingerprint");
    }
}