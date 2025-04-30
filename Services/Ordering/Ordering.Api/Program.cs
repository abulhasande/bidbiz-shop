using Ordering.Api;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//Add Services to the container.

builder.Services
       .AddApplicationServices()
       .AddInfrastructureServices(builder.Configuration)
       .AddApiServices();

var app = builder.Build();

//Configure the HTTP Pipeline.

app.Run();
