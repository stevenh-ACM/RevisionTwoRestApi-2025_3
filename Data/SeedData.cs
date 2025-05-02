#nullable disable

using Microsoft.EntityFrameworkCore;

using RevisionTwoApp.RestApi.Models;

namespace RevisionTwoApp.RestApi.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using(AppDbContext context = new AppDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
        {
            if(context == null || context.Credentials == null)
            {
                throw new ArgumentNullException("Null  AppDbContext");
            }

            context.Database.EnsureDeleted();

            context.Database.EnsureCreated();

            // Look for any entries.
            if(context.Credentials.Any())
            {
                return;   // DB has been seeded
            }

            context.Credentials.AddRange(
                new Credential
                {
                    IsChecked = true,
                    SiteUrl = "http://localhost/acumaticaerp",
                    UserName = "admin",
                    Password = "123",
                    Tenant = "Company",
                    Branch = "",
                    Locale = "en-US"

                }
            );
            context.SaveChanges();
        }
    }
}