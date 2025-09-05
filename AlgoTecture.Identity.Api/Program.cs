using System;
using Algotecture.Identity.Application.Handlers;
using Algotecture.Identity.Contracts.Events;
using Algotecture.Identity.Infrastructure;
using Algotecture.Identity.Infrastructure.Consumers;
using Algotecture.Identity.Infrastructure.Persistence;
using Algotecture.IdentityService.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("hosting.json", optional: true, reloadOnChange: true);
}

var cfg = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDbContextFactory<IdentityDbContext>, IdentityRuntimeContextFactory>();

builder.Services.AddDbContext<IdentityDbContext>(options =>
{
    IdentityRuntimeContextFactory.ConfigureOptions((DbContextOptionsBuilder<IdentityDbContext>)options);
});

builder.Services.AddMediatR(configuration => 
{
    configuration.RegisterServicesFromAssembly(typeof(TelegramLoginHandler).Assembly);
});
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<TelegramLoginValidator>();
builder.Services.AddLogging(configure => 
{
    configure.AddConsole(); // Добавляем вывод в консоль
    configure.SetMinimumLevel(LogLevel.Debug); // Устанавливаем уровень логирования
});

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    x.AddConsumer<UserCreatedConsumer>();
    x.UsingRabbitMq((ctx, mq) =>
    {
        mq.Host(cfg["Rabbit:Host"] ?? "localhost", h =>
        {
            h.Username(cfg["Rabbit:Username"] ?? "guest");
            h.Password(cfg["Rabbit:Password"] ?? "guest");
        });
        mq.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
        mq.Message<IdentityCreated>(e =>
        {
            e.SetEntityName("identity-events"); 
        });
        mq.PublishTopology.GetMessageTopology<IdentityCreated>().ExchangeType = "topic";
        mq.Send<IdentityCreated>(e => 
            e.UseRoutingKeyFormatter((SendContext<IdentityCreated> context) => "identity.*"));
        mq.ConfigureEndpoints(ctx);
    });
    x.AddEntityFrameworkOutbox<IdentityDbContext>(o =>
    {
        o.QueryDelay = TimeSpan.FromSeconds(1); // задержка перед публикацией
        o.UsePostgres();                       // storage provider
        o.DisableInboxCleanupService();        // опционально
        o.UseBusOutbox();                      // обязательная часть
    });
    
});

var app = builder.Build();

app.MapControllers();

if (builder.Environment.IsDevelopment())
{
    app.UseSwagger(); app.UseSwaggerUI();
}
app.Run();