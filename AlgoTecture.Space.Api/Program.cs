using AlgoTecture.Space.Application.Handlers;
using AlgoTecture.Space.Infrastructure;
using AlgoTecture.Space.Infrastructure.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("hosting.json", optional: true, reloadOnChange: true);

var cfg = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDbContextFactory<SpaceDbContext>, SpaceRuntimeContextFactory>();

builder.Services.AddDbContext<SpaceDbContext>(options =>
{
    SpaceRuntimeContextFactory.ConfigureOptions((DbContextOptionsBuilder<SpaceDbContext>)options);
});

builder.Services.AddMediatR(configuration => 
{
    configuration.RegisterServicesFromAssembly(typeof(GetSpacesByTypeQueryHandler).Assembly);
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<GetSpacesByTypeQueryHandler>();

var app = builder.Build();

app.MapControllers();

if (builder.Environment.IsDevelopment())
{
    app.UseSwagger(); app.UseSwaggerUI();
}
app.Run();