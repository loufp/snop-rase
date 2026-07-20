using snoperase.Application;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

builder.Services
    .AddApi()
    .AddInfastructure()
    .AddApplication();

builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.Run();
