namespace GPMLogin.Apis.Supplements;

internal static class ModifyProfileBodyExtensions
{
    internal static ModifyProfileBody ToApiModifyProfileBody(this ModifyProfileRequest request)
    {
        return new ModifyProfileBody(request.ProfileName,
            request.GroupId,
            request.RawProxy,
            request.StartupUrls,
            request.Note,
            request.Color,
            request.UserAgent);
    }
}