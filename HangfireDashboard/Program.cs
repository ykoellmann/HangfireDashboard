using Hangfire;
using Hangfire.Dashboard;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

var dbConnectionString = builder.Configuration.GetConnectionString("DbConnection");

// Configure Hangfire to use SQL Server for storage
builder.Services.AddHangfire(config => config
    .UsePostgreSqlStorage(dbConnectionString));
builder.Services.AddHangfireServer(); // Add the background job server

var app = builder.Build();

// Use the Hangfire Dashboard
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = [new AllowAllDashboardAuthorizationFilter()] // Optional: customize authorization
});

app.MapGet("/", () => "Hangfire Server is running...");

await app.RunAsync();

// Custom authorization filter for the dashboard (optional)
public class AllowAllDashboardAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context) => true; // Open to everyone, restrict in production
}