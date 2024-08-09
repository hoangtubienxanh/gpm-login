namespace GPMLogin.Apis.Supplements;

public class StartProfileRequest
{
    /// <summary>
    ///     <para>
    ///         Additional arguments to pass to the browser instance. The list of Chromium flags
    ///         can be found <a href="https://peter.sh/experiments/chromium-command-line-switches/">here</a>.
    ///     </para>
    /// </summary>
    /// <remarks>
    ///     <para>Use custom browser args at your own risk.</para>
    /// </remarks>
    public ICollection<string>? CommandLineSwitches { get; init; }

    public double? DpiScaling { get; init; }
    public Tuple<int, int>? StartupLocation { get; init; }
    public Tuple<int, int>? WindowSize { get; init; }

    public void Deconstruct(out ICollection<string>? commandLineSwitches, out double? dpiScaling,
        out Tuple<int, int>? startupLocation, out Tuple<int, int>? windowSize)
    {
        commandLineSwitches = CommandLineSwitches;
        dpiScaling = DpiScaling;
        startupLocation = StartupLocation;
        windowSize = WindowSize;
    }
}