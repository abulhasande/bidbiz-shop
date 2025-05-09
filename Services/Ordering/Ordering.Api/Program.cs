using Ordering.Api;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

//Add Services to the container.

builder.Services
       .AddApplicationServices(builder.Configuration)
       .AddInfrastructureServices(builder.Configuration)
       .AddApiServices(builder.Configuration);

var app = builder.Build();

//Configure the HTTP Pipeline.
app.UseApiServices();

if(app.Environment.IsDevelopment())
{
    await app.InitialisedDatabaseAsync();
}

app.Run();
