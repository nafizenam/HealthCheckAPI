using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace UBBL.API.HealthCheck;

public class HealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)    
    {      
        return Task.FromResult(HealthCheckResult.Healthy("This could be a service"));    
    }  
    /*public static void ConfigureHealthChecks(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddSqlServer(configuration["ConnectionStrings:UBBLDbConnStr"], healthQuery: "select 1", name: "SQL Server", failureStatus: HealthStatus.Unhealthy, tags: new[] { "Feedback", "Database" });

    //services.AddHealthChecksUI();
        services.AddHealthChecksUI(opt =>
        {
            opt.SetEvaluationTimeInSeconds(10); //time in seconds between check    
            opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks    
            opt.SetApiMaxActiveRequests(1); //api requests concurrency    
            opt.AddHealthCheckEndpoint("feedback api", "/api/health"); //map health check api    

        })
            .AddInMemoryStorage();
}*/
}
