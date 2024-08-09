using System.Globalization;
using GPMLogin.Helpers;

namespace GPMLogin.Apis.Supplements;

internal static class StartProfileParameterExtensions
{
    internal static string ToApiStartProfileParameter(this StartProfileRequest request)
    {
        var (commandLineSwitches, dpiScaling, startupLocation, windowSize) = request;

        var kv = new List<KeyValuePair<string, string>>();

        if (commandLineSwitches is { Count: > 0 })
            kv.Add(new KeyValuePair<string, string>("addination_args", string.Join(" ", commandLineSwitches)));

        if (dpiScaling is { } dpiScalingValue and <= 1.0 and >= 0)
            kv.Add(
                new KeyValuePair<string, string>("win_scale", dpiScalingValue.ToString(CultureInfo.InvariantCulture)));

        if (startupLocation is { Item1: var x, Item2: var y })
            kv.Add(new KeyValuePair<string, string>("win_pos", $"{x},{y}"));

        if (windowSize is { Item1: var width, Item2: var height })
            kv.Add(new KeyValuePair<string, string>("win_size", $"{width},{height}"));

        return ParameterUrlEncodedContent.GetContentString(kv);
    }
}