using GPMLogin;
using GPMLogin.WebApi;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var configuration = builder.Configuration;

services.TryAddSingleton(TimeProvider.System);

services.AddGPMLoginClient(configuration.GetRequiredSection("gpmloginapp").Bind);
services.AddHttpClient("remote-debugging-address").AddStandardResilienceHandler();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapBrowserAgentApi();

app.Run();