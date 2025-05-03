
/// <summary>
/// Revision Two REST API 2025 Demo Application
/// Version 3.0.0
/// </summary>
/// 
using Microsoft.EntityFrameworkCore;

using RevisionTwoApp.RestApi.Data;

/// <summary>
/// Version 3.0.0
/// This initializes a WebApplicationBuilder object, which is used to configure the application and its services.
/// </summary>
var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("ConnectionString")
    ?? throw new InvalidOperationException("Connection string 'ConnectionString' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<DemoUser>(options => options.SignIn.RequireConfirmedAccount = false)
//    .AddEntityFrameworkStores<AppDbContext>();

//builder.Services.AddDbContext<InMemoryDbContext> ( options => options.UseInMemoryDatabase ( "InMemoryDbContext" ) );

builder.Logging.AddSimpleConsole();

builder.Services.AddRazorPages();

var app = builder.Build();

//Seed AcuCredentials
using(var scope = app.Services.CreateScope())
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
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();