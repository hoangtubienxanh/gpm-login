namespace GPMLogin.Apis.Supplements;

public record ModifyProfileRequest(
    string ProfileName,
    int GroupId,
    string RawProxy,
    string StartupUrls,
    string Note,
    string Color,
    string UserAgent);