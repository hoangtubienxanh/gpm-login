using System.Text;

namespace GPMLogin.Helpers;

internal static class ParameterUrlEncodedContent
{
    internal static string GetContentString(
        IEnumerable<KeyValuePair<string, string>> nameValueCollection)
    {
        ArgumentNullException.ThrowIfNull(nameValueCollection, nameof(nameValueCollection));
        StringBuilder stringBuilder = new();
        foreach (var nameValue in nameValueCollection)
        {
            if (stringBuilder.Length > 0) stringBuilder.Append('&');

            stringBuilder.Append(Encode(nameValue.Key));
            stringBuilder.Append('=');
            stringBuilder.Append(Encode(nameValue.Value));
        }

        return stringBuilder.ToString();
    }

    internal static string Encode(string data)
    {
        return string.IsNullOrEmpty(data) ? string.Empty : Uri.EscapeDataString(data).Replace("%20", "+");
    }
}