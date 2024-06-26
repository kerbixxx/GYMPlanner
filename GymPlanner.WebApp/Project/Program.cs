using GymPlanner.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using GymPlanner.Infrastructure;
using GymPlanner.Application;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.SignalR;
using GymPlanner.WebUI.Hubs;
using Serilog;
using System.Reflection;
using GymPlanner.Application.Configurations;
using System.Configuration;
using GymPlanner.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PlanDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer"));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => 
    {
        options.LoginPath = new PathString("/Account/Login");
        options.AccessDeniedPath = new PathString("/Account/Login");
    });

builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});

builder.Services.Configure<DefaultNamesOptions>(builder.Configuration.GetSection("DefaultNames"));
builder.Services.AddSingleton<IUserIdProvider, UserIdProvider>();
builder.Services.AddInfrastructureServices();
builder.Services.AddRepositories();
builder.Services.AddCustomServices();
builder.Services.AddApplication();

Log.Logger = new LoggerConfiguration()
    .WriteTo
    .MSSqlServer(
    connectionString: builder.Configuration.GetConnectionString("Logging"),
    sinkOptions:new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions { TableName = "TableName"})
    .CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/chat");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();