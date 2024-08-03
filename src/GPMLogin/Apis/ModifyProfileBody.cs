namespace GPMLogin.Apis;

public sealed record ModifyProfileBody(
    string ProfileName,
    int GroupId,
    string RawProxy,
    string StartupUrls,
    string Note,
    string Color,
    string UserAgent
);