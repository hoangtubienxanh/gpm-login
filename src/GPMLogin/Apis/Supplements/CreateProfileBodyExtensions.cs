namespace GPMLogin.Apis.Supplements;

/// <summary>
///     Provides extension methods for converting <see cref="CreateProfileRequest" /> to <see cref="CreateProfileBody" />.
/// </summary>
internal static class CreateProfileBodyExtensions
{
    internal static CreateProfileBody ToApiCreateProfileBody(this CreateProfileRequest request)
    {
        return new CreateProfileBody
        {
            ProfileName = request.DisplayName,
            GroupName = request.GroupName,
            BrowserCore = request.BrowserType,
            BrowserName = null!,
            BrowserVersion = request.BrowserVersion,
            IsRandomBrowserVersion = request.BrowserVersion is not null,
            RawProxy = request.ProxyConnectionString,
            StartupUrls = request.NewPageUrls is { Length: >= 1 } ? string.Join(" ", request.NewPageUrls) : null,
            IsMaskedFont = request.HasModifiedFontList,
            IsNoiseCanvas = request.HasModifiedCanvasData,
            IsNoiseWebgl = request.HasModifiedWebGLData,
            IsNoiseClientRect = request.HasModifiedDomRect,
            IsNoiseAudioContext = request.HasModifiedAudioContext,
            IsRandomScreen = request.HasModifiedScreenSize,
            IsMaskedWebglData = request.HasModifiedWebGLParameter,
            IsMaskedMediaDevice = request.HasModifiedMediaDevice,
            Os = request.PlatformOs,
            IsRandomOs = request.PlatformOs is not null,
            WebrtcMode = request.WebrtcMode.HasValue ? (int?)request.WebrtcMode.Value : null,
            UserAgent = request.UserAgent
        };
    }
}