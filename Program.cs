#nullable disable

#pragma warning disable CS1572 // XML comment has badly formed XML
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1587 // Non-nullable field is uninitialized. Consider declaring as nullable.

global using Acumatica.Default_24_200_001.Model;
global using Acumatica.RESTClient.AuthApi;
global using Acumatica.RESTClient.Client;
global using Acumatica.RESTClient.ContractBasedApi;
global using Acumatica.RESTClient.ContractBasedApi.Model;

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.RazorPages;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Diagnostics;

global using RevisionTwoApp.RestApi.Auxiliary;
global using RevisionTwoApp.RestApi.Data;
global using RevisionTwoApp.RestApi.DTOs.Conversions;
global using RevisionTwoApp.RestApi.Helper;
global using RevisionTwoApp.RestApi.Models;
global using RevisionTwoApp.RestApi.Models.App;
global using RevisionTwoApp.RestApi.Settings;

using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;

/// <summary>
/// Version 3.0.0
/// This initializes a WebApplicationBuilder object, which is used to configure the application and its services.
/// </summary>
var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders().AddConsole();

//builder.Services.AddHealthChecks();

//builder.Services.AddHttpLogging(logging => 
//{
//    logging.LoggingFields = HttpLoggingFields.All;
//    logging.RequestHeaders.Add("sec-ch-ua");
//    logging.ResponseHeaders.Add("MyResponseHeader");
//    logging.MediaTypeOptions.AddText("application/javascript");
//    logging.RequestBodyLogLimit = 4096;
//    logging.ResponseBodyLogLimit = 4096;
//    logging.CombineLogs = true;
//});

//builder.Services.AddDbContext<InMemoryDbContext> ( options => options.UseInMemoryDatabase ( "InMemoryDbContext" ) );

var connectionString = builder.Configuration.GetConnectionString("ConnectionString")
    ?? throw new InvalidOperationException("Connection string 'ConnectionString' not found.");

builder.Services.AddDbContext<AppDbContext>(options => options
                .UseSqlServer(connectionString)
                .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning))
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging());

builder.Services.AddDefaultIdentity<IdentityUser>(options => options
                .SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.ConfigureApplicationCookie(o =>
{
    o.ExpireTimeSpan = TimeSpan.FromDays(5);
    o.SlidingExpiration = true;
});

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

builder.Services.Configure<PasswordHasherOptions>(option =>
{
    option.IterationCount = 12000;
});
 
Globals.InitializeProperties();

builder.Services.AddRazorPages();

var app = builder.Build();

//app.MapHealthChecks("/healthz");

//app.UseHttpLogging();

//Seed credentials for Acumatica ERP connection
// This is a one-time operation to seed the database with initial data.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if(!app.Environment.IsDevelopment())
{
    app.Logger.LogInformation("Production Environment");
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.Logger.LogInformation("Development Environment");
    app.UseMigrationsEndPoint();
}
/// <summary> HTTP request pipeline </summary>
app.UseHttpsRedirection();

/// <summary> Static files in wwwroot are made available </summary>
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

/// <summary> Map Razor Pages </summary> 
app.MapRazorPages();

/// <summary> Application Start </summary>   
app.Run();