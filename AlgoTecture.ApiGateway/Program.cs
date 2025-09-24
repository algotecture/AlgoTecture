using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("hosting.json", optional: true, reloadOnChange: true);
builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
var app = builder.Build();
app.MapReverseProxy();
app.Run();