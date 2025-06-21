using Microsoft.EntityFrameworkCore;
using ProductService.Application.Mappings;
using ProductService.Application.UseCases;
using ProductService.Application.UseCases.Interfaces;
using ProductService.Infrastructure.Extensions;
using ProductService.Infrastructure.Persistence.DataContext;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Inject use cases
builder.Services.AddScoped<ICRUDProductUseCase, CRUDProductUseCase>();

// Db context
ProductInfrastructureServiceRegistration.AddInfrastructure(builder.Services, builder.Configuration);

// Add Auto Mapper
builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);

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
    var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
