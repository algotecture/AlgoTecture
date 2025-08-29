using Algotecture.Identity.Application.Handlers;
using Algotecture.Identity.Infrastructure.Consumers;
using Algotecture.Identity.Infrastructure.Persistence;
using Algotecture.IdentityService.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var cfg = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IdentityDbContext>(o =>
    o.UseNpgsql(cfg.GetConnectionString("IdentityDb")));

builder.Services.AddMediatR(configuration => 
{
    configuration.RegisterServicesFromAssembly(typeof(TelegramLoginHandler).Assembly);
});
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<TelegramLoginValidator>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<UserCreatedConsumer>();
    x.UsingRabbitMq((ctx, mq) =>
    {
        mq.Host(cfg["Rabbit:Host"] ?? "localhost", h =>
        {
            h.Username(cfg["Rabbit:Username"] ?? "guest");
            h.Password(cfg["Rabbit:Password"] ?? "guest");
        });
        mq.ConfigureEndpoints(ctx);
    });
});

var app = builder.Build();
app.UseSwagger(); app.UseSwaggerUI();
app.MapControllers();
app.Run();