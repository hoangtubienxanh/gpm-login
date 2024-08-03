using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GPMLogin;

public static class GPMLoginServiceCollectionExtensions
{
    private static readonly string SdkVersion =
        typeof(GPMLoginClient).Assembly.GetName().Version?.ToString(3) ??
        // This should never happen, unless the assembly had its metadata trimmed
        "unknown";

    /// <summary>
    ///     Adds and configures GPMLogin-related services.
    /// </summary>
    public static IServiceCollection AddGPMLoginClient(
        this IServiceCollection services,
        Action<GPMLoginOptions> configureOptions)
    {
        services.AddOptions<GPMLoginOptions>()
            .Configure(configureOptions)
            .Validate(options => !string.IsNullOrEmpty(options.ApiUrl), "GPMLogin: Missing ApiUrl");

        services.RegisterDependencies();

        return services;
    }

    private static void RegisterDependencies(this IServiceCollection services)
    {
        services.AddTransient<GPMLoginHttpHandler>();

        services.AddHttpClient<IGPMLoginClient, GPMLoginClient>((http, sp) =>
        {
            var options = sp.GetRequiredService<IOptionsMonitor<GPMLoginOptions>>().CurrentValue;

            http.BaseAddress = new Uri(options.ApiUrl);
            http.DefaultRequestHeaders.Add("Client-Version", $".NET-{SdkVersion}");

            return new GPMLoginClient(http, options);
        }).AddHttpMessageHandler<GPMLoginHttpHandler>();
    }
}