using GPMLogin.Apis.Supplements.Enums;

namespace GPMLogin.Apis.Supplements;

/// <summary>
///     Represents a request to create a profile.
/// </summary>
public class CreateProfileRequest
{
    /// <summary>
    ///     A human-palatable name for the browser profile.Chave value
    /// </summary>
    public required string DisplayName { get; init; }

    /// <summary>
    ///     An identifier for a group.
    ///     If not provided or if the group doesn't exist, the profile will be placed into the default group.
    /// </summary>
    public string? GroupName { get; init; }

    /// <summary>
    ///     Browser engine type.
    ///     Supported values are "chromium", "firefox"
    ///     If not provided, default to "chromium"
    /// </summary>
    public string? BrowserType { get; init; }

    /// <summary>
    ///     Gets or sets the browser version of the profile.
    /// </summary>
    public string? BrowserVersion { get; init; }

    /// <summary>
    ///     Gets or sets a value indicating whether the profile has a random browser version.
    /// </summary>
    public bool? HasRandomBrowserVersion { get; init; }

    /// <summary>
    ///     Gets or sets the proxy connection string of the profile.
    /// </summary>
    public string? ProxyConnectionString { get; init; }

    /// <summary>
    ///     Gets or sets the new page URLs of the profile.
    /// </summary>
    public string[]? NewPageUrls { get; init; }

    /// <summary>
    ///     Gets or sets a value indicating whether the profile has a modified font list.
    /// </summary>
    public bool? HasModifiedFontList { get; init; }

    /// <summary>
    ///     Gets or sets a value indicating whether the profile has modified canvas data.
    /// </summary>
    public bool? HasModifiedCanvasData { get; init; }

    /// <summary>
    ///     Gets or sets a value indicating whether the profile has modified WebGL data.
    /// </summary>
    public bool? HasModifiedWebGLData { get; init; }

    /// <summary>
    ///     Gets or sets a value indicating whether the profile has modified DOM rect.
    /// </summary>
    public bool? HasModifiedDomRect { get; init; }

    /// <summary>
    ///     Gets or sets a value indicating whether the profile has modified audio context.
    /// </summary>
    public bool? HasModifiedAudioContext { get; init; }

    /// <summary>
    ///     Gets or sets a value indicating whether the profile has modified screen size.
    /// </summary>
    public bool? HasModifiedScreenSize { get; init; }

    /// <summary>
    ///     Gets or sets a value indicating whether the profile has modified WebGL parameter.
    /// </summary>
    public bool? HasModifiedWebGLParameter { get; init; }

    /// <summary>
    ///     Gets or sets a value indicating whether the profile has modified media device.
    /// </summary>
    public bool? HasModifiedMediaDevice { get; init; }

    /// <summary>
    ///     Gets or sets the platform OS of the profile.
    /// </summary>
    public string? PlatformOs { get; init; }

    /// <summary>
    ///     Gets or sets the WebRTC mode of the profile.
    /// </summary>
    public WebRTCType? WebrtcMode { get; init; }

    /// <summary>
    ///     Gets or sets the user agent of the profile.
    /// </summary>
    public string? UserAgent { get; init; }

    /// <summary>
    ///     Deconstructs the <see cref="CreateProfileRequest" /> into individual properties.
    /// </summary>
    /// <param name="displayName">The name of the profile.</param>
    /// <param name="groupName">The group name of the profile.</param>
    /// <param name="browserType">The browser type of the profile.</param>
    /// <param name="browserVersion">The browser version of the profile.</param>
    /// <param name="hasRandomBrowserVersion">A value indicating whether the profile has a random browser version.</param>
    /// <param name="proxyConnectionString">The proxy connection string of the profile.</param>
    /// <param name="newPageUrls">The new page URLs of the profile.</param>
    /// <param name="hasModifiedFontList">A value indicating whether the profile has a modified font list.</param>
    /// <param name="hasModifiedCanvasData">A value indicating whether the profile has modified canvas data.</param>
    /// <param name="hasModifiedWebGLData">A value indicating whether the profile has modified WebGL data.</param>
    /// <param name="hasModifiedDomRect">A value indicating whether the profile has modified DOM rect.</param>
    /// <param name="hasModifiedAudioContext">A value indicating whether the profile has modified audio context.</param>
    /// <param name="hasModifiedScreenSize">A value indicating whether the profile has modified screen size.</param>
    /// <param name="hasModifiedWebGLParameter">A value indicating whether the profile has modified WebGL parameter.</param>
    /// <param name="hasModifiedMediaDevice">A value indicating whether the profile has modified media device.</param>
    /// <param name="platformOs">The platform OS of the profile.</param>
    /// <param name="webrtcMode">The WebRTC mode of the profile.</param>
    /// <param name="userAgent">The user agent of the profile.</param>
    public void Deconstruct(out string displayName, out string? groupName, out string? browserType,
        out string? browserVersion, out bool? hasRandomBrowserVersion, out string? proxyConnectionString,
        out string[]? newPageUrls, out bool? hasModifiedFontList, out bool? hasModifiedCanvasData,
        out bool? hasModifiedWebGLData, out bool? hasModifiedDomRect, out bool? hasModifiedAudioContext,
        out bool? hasModifiedScreenSize, out bool? hasModifiedWebGLParameter, out bool? hasModifiedMediaDevice,
        out string? platformOs, out WebRTCType? webrtcMode, out string? userAgent)
    {
        displayName = DisplayName;
        groupName = GroupName;
        browserType = BrowserType;
        browserVersion = BrowserVersion;
        hasRandomBrowserVersion = HasRandomBrowserVersion;
        proxyConnectionString = ProxyConnectionString;
        newPageUrls = NewPageUrls;
        hasModifiedFontList = HasModifiedFontList;
        hasModifiedCanvasData = HasModifiedCanvasData;
        hasModifiedWebGLData = HasModifiedWebGLData;
        hasModifiedDomRect = HasModifiedDomRect;
        hasModifiedAudioContext = HasModifiedAudioContext;
        hasModifiedScreenSize = HasModifiedScreenSize;
        hasModifiedWebGLParameter = HasModifiedWebGLParameter;
        hasModifiedMediaDevice = HasModifiedMediaDevice;
        platformOs = PlatformOs;
        webrtcMode = WebrtcMode;
        userAgent = UserAgent;
    }
}