using GPMLogin.Helpers;

namespace GPMLogin.Apis.Supplements;

/// <summary>
///     Provides extension methods for URL-encoding the parameters of a <see cref="GetProfilesRequest" />.
/// </summary>
internal static class GetProfileParameterExtensions
{
    internal static string ToApiGetProfileParameter(this GetProfilesRequest request)
    {
        var (groupName, subStringMatch, sortType, pageSize, pageIndex) = request;

        var kv = new List<KeyValuePair<string, string>>();

        if (groupName != null) kv.Add(new KeyValuePair<string, string>("group", groupName));

        if (subStringMatch != null) kv.Add(new KeyValuePair<string, string>("search", subStringMatch));

        if (sortType is { } sortTypeValue) kv.Add(new KeyValuePair<string, string>("sort", sortTypeValue.ToString()));

        if (pageIndex is { } pageIndexValue)
            kv.Add(new KeyValuePair<string, string>("page", pageIndexValue.ToString()));

        if (pageSize is { } pageSizeValue)
            kv.Add(new KeyValuePair<string, string>("per_page", pageSizeValue.ToString()));

        return ParameterUrlEncodedContent.GetContentString(kv);
    }
}