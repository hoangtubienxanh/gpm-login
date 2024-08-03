using GPMLogin.Apis.Supplements.Enums;

namespace GPMLogin.Apis.Supplements;

public class GetProfilesRequest
{
    public string? GroupId { get; init; }
    public string? SubStringMatch { get; init; }

    /// <summary>
    ///     Sort order.
    ///     0 - Newest, 1 - Oldest to newest, 2 - Name A-Z, 3 - Name Z-A.
    /// </summary>
    public SortType? SortType { get; init; }

    /// <summary>
    ///     Number of profiles per page. Defaults to <c>50</c>.
    /// </summary>
    public int? PageSize { get; init; }

    /// <summary>
    ///     Page number. Defaults to <c>1</c>.
    /// </summary>
    public int? PageIndex { get; init; }

    public void Deconstruct(out string? groupName, out string? subStringMatch, out SortType? sortType,
        out int? pageSize, out int? pageIndex)
    {
        groupName = GroupId;
        subStringMatch = SubStringMatch;
        sortType = SortType;
        pageSize = PageSize;
        pageIndex = PageIndex;
    }
}