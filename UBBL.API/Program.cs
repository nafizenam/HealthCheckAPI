using HealthChecks.UI.Client;
using HealthChecks.UI.Configuration;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using UBBL.API;
using UBBL.API.HealthCheck;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//Congiguring Health Ckeck
//builder.Services.ConfigureHealthChecks(builder.Configuration);
builder.Services.AddHealthChecks().AddCheck<HealthCheck>("HealthCheck", tags: new[] { "First_Health_Check" });
builder.Services.AddHealthChecks().AddCheck<ServerHealthCheck>("general", tags: ["all"]);
builder.Services.AddHealthChecks().AddCheck<ServerHealthCheck>("Server", tags: ["all", "subsystems"]);
builder.Services.AddHealthChecks().AddCheck<DatabaseConnectionCheck>("DatabaseConnectionCheck",tags:new[]{"Database_Connection_Check"});
//builder.Services.AddHealthChecks();
builder.Services.AddHealthChecksUI().AddInMemoryStorage();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//HealthCheck Middleware
app.MapHealthChecks("/api/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    
});
app.MapHealthChecksUI();
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
