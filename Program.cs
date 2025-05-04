#pragma warning disable CS1572 // XML comment has badly formed XML
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1587 // Non-nullable field is uninitialized. Consider declaring as nullable.

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using RevisionTwoApp.RestApi.Data;
using RevisionTwoApp.RestApi.Models;

/// <summary>
/// Version 3.0.0
/// This initializes a WebApplicationBuilder object, which is used to configure the application and its services.
/// </summary>
var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<InMemoryDbContext> ( options => options.UseInMemoryDatabase ( "InMemoryDbContext" ) );

var connectionString = builder.Configuration.GetConnectionString("ConnectionString")
    ?? throw new InvalidOperationException("Connection string 'ConnectionString' not found.");

builder.Services.AddDbContext<AppDbContext>(options => options
            .UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<DemoUser>(options => options
            .SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<AppDbContext>();


builder.Logging.AddSimpleConsole();

//builder.Services.ConfigureApplicationCookie(o => {
//            o.ExpireTimeSpan = TimeSpan.FromDays(5);
//            o.SlidingExpiration = true;
//});

//// Force Identity's security stamp to be validated every minute.
//builder.Services.Configure<SecurityStampValidatorOptions>(o => 
//            o.ValidationInterval = TimeSpan.FromMinutes(4));

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 1;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ012g3456789-._@+";
    options.User.RequireUniqueEmail = false;
});

//builder.Services.ConfigureApplicationCookie(options =>
//{
//            options.AccessDeniedPath = "/Identity/Account/AccessDenied";
//            options.Cookie.Name = "RevisionTwoRestApi";
//            options.Cookie.HttpOnly = true;
//            options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
//            options.LoginPath = "/Identity/Account/Login";
//            options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
//            options.SlidingExpiration = true;
//});

//builder.Services.Configure<PasswordHasherOptions>(option =>
//{
//            option.IterationCount = 12000;
//});

builder.Services.AddRazorPages();

var app = builder.Build();

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