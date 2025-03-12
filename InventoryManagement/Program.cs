using InventoryManagement.Models;
using Microsoft.EntityFrameworkCore;

using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using InventoryManagement.Services;
using InventoryManagement.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DI for DbContext
builder.Services.AddDbContext<TransactionDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));


// Add Hangfire services
builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DevConnection")));
builder.Services.AddHangfireServer();

builder.Services.AddSingleton<DatabaseService>(provider =>
    new DatabaseService(builder.Configuration.GetConnectionString("DevConnection")));

builder.Services.AddSignalR();
builder.Services.AddScoped<SignalrServices>();


var app = builder.Build();
    
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.UseHangfireDashboard(); // Enable Hangfire Dashboard

app.MapControllers();
app.MapHangfireDashboard(); // Route for Hangfire UI


// Map SignalR Hub
app.MapHub<MyHub>("/myhub");

using var scope = app.Services.CreateScope();
var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<MyHub>>();



// Start the recurring job
RecurringJob.AddOrUpdate<DatabaseService>("summarize-daily",
    service => service.ExecuteStoredProcedure("[InvMgnt].[dbo].[SummarizeCusPurchaseDaily]"),
    "*/5 * * * * *"); // Run every 5 seconds by using Cron Expression

RecurringJob.AddOrUpdate<DatabaseService>("simulateNewPurchase",
    service => service.ExecuteStoredProcedure("[InvMgnt].[dbo].[SimulateNewPurchase]"),
    "*/5 * * * * *"); // Run every 5 seconds by using Cron Expression



RecurringJob.AddOrUpdate<SignalrServices>(
    "RefreshPurchase",
    s => s.RefreshPage(),
    "*/5 * * * * *" // Runs every 5 seconds
);








app.Run();
