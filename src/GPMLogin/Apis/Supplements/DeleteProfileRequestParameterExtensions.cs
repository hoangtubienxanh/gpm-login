namespace GPMLogin.Apis.Supplements;

public static class DeleteProfileRequestParameterExtensions
{
    internal static string ToDeleteProfileParameter(this DeleteProfileRequest request)
    {
        return $"mode={(int)request.DeleteMode}";
    }
}