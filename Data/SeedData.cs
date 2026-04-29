#nullable disable

namespace RevisionTwoApp.RestApi.Data;

#region SeedData
/// <summary>
/// Provides methods to seed initial data into the database.
/// </summary>
public static class SeedData {
#region methods
  /// <summary>
  /// Initializes the database with seed data.
  /// </summary>
  /// <param name="serviceProvider">The service provider to resolve
  /// dependencies.</param> <exception cref="ArgumentNullException">Thrown when
  /// the AppDbContext or its properties are null.</exception>
  public static void Initialize(IServiceProvider serviceProvider) {
    using (AppDbContext context = new AppDbContext(
               serviceProvider
                   .GetRequiredService<DbContextOptions<AppDbContext>>())) {
      if (context == null || context.Credentials == null) {
        throw new ArgumentNullException("Null AppDbContext");
      }

      _ = context.Database.EnsureDeleted();
      _ = context.Database.EnsureCreated();

      // Look for any entries.
      if (context.Credentials.Any()) {
        return; // DB has been seeded
      }

      context.Credentials.AddRange(
          new Credential { IsChecked = false,
                           SiteUrl = "http://localhost/acumaticaerp",
                           UserName = "admin", Password = "123",
                           Tenant = "Company", Branch = "", Locale = "en-US" });
      _ = context.SaveChanges();

      context.Credentials.AddRange(
          new Credential { IsChecked = true,
                           SiteUrl = "https://acudemos.us/acumaticaerp",
                           UserName = "admin", Password = "123",
                           Tenant = "Company", Branch = "", Locale = "en-US" });
      _ = context.SaveChanges();
    }
  }
#endregion
}
#endregion
