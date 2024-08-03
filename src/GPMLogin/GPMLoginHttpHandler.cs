using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace GPMLogin;

public class GPMLoginHttpHandler(ILogger<GPMLoginHttpHandler> logger)
    : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        logger.LogResponseInterceptionStarted(request.RequestUri?.ToString());

        var content = await response.Content.ReadFromJsonAsync<JsonElement>(cancellationToken);
        if (!content.TryGetProperty("success", out var success) || !success.GetBoolean())
        {
            _ = content.TryGetProperty("message", out var messageValue);

            // Convert the error body to a problem detail
            var problemDetails = new GPMLoginProblemDetails(
                "https://docs.gpmloginapp.com/api-document",
                "Bad Request",
                (int)response.StatusCode,
                messageValue.GetString() ?? string.Empty,
                request.RequestUri?.ToString()
            );

            throw new GPMLoginApiException(problemDetails);
        }

        if (!content.TryGetProperty("data", out var dataContext))
        {
            logger.LogUnhandledException();
            throw new InvalidOperationException("Failed to deserialize the response body content.");
        }

        response.Content = JsonContent.Create(dataContext);
        return response;
    }
}