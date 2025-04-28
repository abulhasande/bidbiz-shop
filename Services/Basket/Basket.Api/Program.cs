using Basket.Api.Data;
using Basket.Api.Models;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Carter;
using HealthChecks.UI.Client;
using Marten;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);

//Add Services to Container

builder.Services.AddCarter();

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviors<,>));
    config.AddOpenBehavior(typeof(LoggingBehviors<,>));
});


builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);

}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CacheBasketRepository>();

//builder.Services.AddScoped<IBasketRepository>(provider =>
//{
//    var basketRepository = provider.GetRequiredService<BasketRepository>();
//    return new CacheBasketRepository(basketRepository, provider.GetRequiredService<IDistributedCache>());
//});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
       .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
       .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

var app = builder.Build();


//Configure the HTTP request pipeline

app.MapCarter();

app.UseExceptionHandler(opts =>{ });
app.UseHealthChecks("/health", 
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
