using System.Text.Json.Serialization;
using GPMLogin.Apis;

namespace GPMLogin;

[JsonSerializable(typeof(ResponseObjectRoot))]
[JsonSerializable(typeof(StartProfileResponse))]
[JsonSerializable(typeof(CreateProfileBody))]
[JsonSerializable(typeof(CreateProfileResponse))]
[JsonSerializable(typeof(ModifyProfileBody))]
[JsonSerializable(typeof(Profile))]
[JsonSerializable(typeof(List<Profile>))]
[JsonSerializable(typeof(List<Group>))]
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    NumberHandling = JsonNumberHandling.Strict,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
internal partial class GPMLoginSerializerContext : JsonSerializerContext;