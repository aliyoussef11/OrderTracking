using Microsoft.EntityFrameworkCore;
using OrderService.Application.Mappings;
using OrderService.Application.UseCases;
using OrderService.Application.UseCases.Interfaces;
using OrderService.Infrastructure.Extensions;
using OrderService.Infrastructure.Persistence.DataContext;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Inject use cases
builder.Services.AddScoped<ICRUDOrderUseCase, CRUDOrderUseCase>();

// Db context
OrderInfrastructureServiceRegistration.AddInfrastructure(builder.Services, builder.Configuration);

// Add Auto Mapper
builder.Services.AddAutoMapper(typeof(OrderProfile).Assembly);

// Serilog
builder.Host.UseSerilog((ctx, config) =>
    config.ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Migrate
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
