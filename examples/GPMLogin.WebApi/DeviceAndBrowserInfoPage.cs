using System.Text.Json;
using PuppeteerAot;

namespace GPMLogin.WebApi;

public class DeviceAndBrowserInfoPage(IPage page)
{
    public async Task<JsonElement?> Handle()
    {
        await page.GoToAsync("https://deviceandbrowserinfo.com/info_device");
        await page.WaitForExpressionAsync("window.fingerprint && window.fingerprint.speechSynthesisVoices",
            new WaitForFunctionOptions
            {
                Timeout = (int)TimeSpan.FromMinutes(1).TotalMilliseconds
            });
        return await page.EvaluateExpressionAsync("window.fingerprint");
    }
}