# GPM-Login .NET SDK

The unofficial GPM-Login .NET library, supporting .NET Core 8.0

## Getting Started

### Install the package

Install the GPMLogin library with [NuGet](https://nuget.org/packages/GPMLogin):

```dotnetcli
dotnet add package GPMLogin
```

### See also

Examples:

- [GPMLogin.WebApi](examples/GPMLogin.WebApi) — crawling website data using GPMLogin and Puppeteer inside an ASP.NET
  Core app

### Usage

💡 See the [API document](https://docs.gpmloginapp.com/api-document) in the official documentation.

💡 See the [How-to integrate with Selenium video guide](https://www.youtube.com/watch?v=zUJAzMzTB4g).

### Resolve the client

Add GPMLogin to your service container:

```csharp
// In Program.cs or Startup.cs
services.AddGPMLoginClient(options => options.ApiUrl = "your_api_url");
```

Inject the client into your service:

```csharp
async Task<List<Profile>> Handler(IGPMLoginClient client)
{
    // Retrieve the 30 most recent profiles
    return await client.GetProfilesAsync(new GetProfilesRequest() { PageSize = 30, SortType = SortType.Newest });
}
```