using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.API;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();
var app = builder.Build();



app.Run();
