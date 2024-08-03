namespace GPMLogin.Apis;

/// <summary>
///     A group item
/// </summary>
/// <param name="Id">The current group Id.</param>
/// <param name="Name">Name of the group.</param>
/// <param name="Sort">
///     The current sorting attribute.
///     <see href="https://docs.gpmloginapp.com/api-document/danh-sach-profiles" />
/// </param>
/// <param name="CreatedBy">The Id of the user who created the group.</param>
/// <param name="CreatedAt">The date when the group was created, in the format <c>yyyy-MM-ddTHH:mm:ss</c>.</param>
/// <param name="UpdatedAt">The date when the group was last updated, in the format <c>yyyy-MM-ddTHH:mm:ss</c>.</param>
public sealed record Group(
    int Id,
    string Name,
    int Sort,
    int CreatedBy,
    string CreatedAt,
    string UpdatedAt
);