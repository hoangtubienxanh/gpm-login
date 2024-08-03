namespace GPMLogin.Apis;

public sealed record CreateProfileBody
{
    public required string ProfileName { get; init; }
    public string? GroupName { get; init; }
    public string? BrowserCore { get; init; }
    public string? BrowserName { get; init; }
    public string? BrowserVersion { get; init; }
    public bool? IsRandomBrowserVersion { get; init; }
    public string? RawProxy { get; init; }
    public string? StartupUrls { get; init; }
    public bool? IsMaskedFont { get; init; }
    public bool? IsNoiseCanvas { get; init; }
    public bool? IsNoiseWebgl { get; init; }
    public bool? IsNoiseClientRect { get; init; }
    public bool? IsNoiseAudioContext { get; init; }
    public bool? IsRandomScreen { get; init; }
    public bool? IsMaskedWebglData { get; init; }
    public bool? IsMaskedMediaDevice { get; init; }
    public bool? IsRandomOs { get; init; }
    public string? Os { get; init; }
    public int? WebrtcMode { get; init; }
    public string? UserAgent { get; init; }
}