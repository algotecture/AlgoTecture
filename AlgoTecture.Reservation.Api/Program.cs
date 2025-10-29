using System.Globalization;
using AlgoTecture.Reservation.Application.Handlers;
using AlgoTecture.Reservation.Infrastructure;
using AlgoTecture.Reservation.Infrastructure.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

var cfg = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDbContextFactory<ReservationDbContext>, ReservationRuntimeContextFactory>();

builder.Services.AddDbContext<ReservationDbContext>(options =>
{
    ReservationRuntimeContextFactory.ConfigureOptions((DbContextOptionsBuilder<ReservationDbContext>)options);
});

builder.Services.AddMediatR(configuration => 
{
    configuration.RegisterServicesFromAssembly(typeof(GetUserReservationsQueryHandler).Assembly);
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<GetUserReservationsQueryHandler>();

var app = builder.Build();

app.MapControllers();

if (builder.Environment.IsDevelopment())
{
    app.UseSwagger(); app.UseSwaggerUI();
}

var defaultCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

app.Run();