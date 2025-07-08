#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using RevisionTwoApp.RestApi.Models;
using RevisionTwoApp.RestApi.Models.App;

namespace RevisionTwoApp.RestApi.Data;

#region AppDbContext
/// <summary>
/// Represents the application's database context, providing access to the database tables and entities.
/// </summary>
public class AppDbContext: IdentityDbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AppDbContext"/> class with the specified options.
    /// </summary>
    /// <param name="options">The options to configure the database context.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }

    /// <summary>
    /// Configures the model for the database context.
    /// </summary>
    /// <param name="modelBuilder">The model builder used to configure the database schema.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // Add your customizations after calling base.OnModelCreating(builder);
        base.OnModelCreating(modelBuilder);

        //Seeding a  'Administrator' role to AspNetRoles table
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "a8e2d65c-d231-490c-80e3-28753a55f6db",
            Name = "Administrator",
            NormalizedName = "ADMINISTRATOR".ToUpper()
        });

        //a hasher to hash the password before seeding the user to the db
        var user = new IdentityUser
        {
            Id = "74462461-2f95-46cf-b4c9-feda3cd43d4a", // primary key
            UserName = "demo@r.c",
            NormalizedUserName = "DEMO@R.C".ToUpper(),
            Email = "demo@r.c",
            NormalizedEmail = "DEMO@R.C".ToUpper(),
            EmailConfirmed = true,
        };

        var hasher = new PasswordHasher<IdentityUser>();
        user.PasswordHash = hasher.HashPassword(user, "123");  
        
        modelBuilder.Entity<IdentityUser>().HasData(user);

        //Seeding the relation between our user and role to AspNetUserRoles table
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "a8e2d65c-d231-490c-80e3-28753a55f6db",
                UserId = "74462461-2f95-46cf-b4c9-feda3cd43d4a"
            }
        );
    }
    #endregion

    #region DbSet Properties
    /// <summary>
    /// Gets or sets the credentials table.
    /// </summary>
    public DbSet<Credential> Credentials { get; set; } = default!;

    /// <summary>
    /// Gets or sets the addresses table.
    /// </summary>
    public DbSet<Address_App> Addresses { get; set; } = default!;

    /// <summary>
    /// Gets or sets the bills table.
    /// </summary>
    public DbSet<Bill_App> Bills { get; set; } = default!;

    /// <summary>
    /// Gets or sets the bill details table.
    /// </summary>
    public DbSet<BillDetail_App> BillDetails { get; set; } = default!;

    /// <summary>
    /// Gets or sets the collection of customers from Acumatica ERP into the local store.
    /// </summary>
    public DbSet<Customer_App> Customers { get; set; } = default!;

    /// <summary>
    /// Gets or sets the contacts table.
    /// </summary>
    public DbSet<Contact_App> Contacts { get; set; } = default!;

    /// <summary>
    /// Gets or sets the cases table.
    /// </summary>
    public DbSet<Case_App> Cases { get; set; } = default!;

    /// <summary>
    /// Gets or sets the opportunities table.
    /// </summary>
    public DbSet<Opportunity_App> Opportunities { get; set; } = default!;

    /// <summary>
    /// Gets or sets the sales orders table.
    /// </summary>
    public DbSet<SalesOrder_App> SalesOrders { get; set; } = default!;

    /// <summary>
    /// Gets or sets the shipments table.
    /// </summary>
    public DbSet<Shipment_App> Shipments { get; set; } = default!;

    /// <summary>
    /// Gets or sets the shipment details table.
    /// </summary>
    public DbSet<ShipmentDetail_App> ShipmentDetails { get; set; } = default!;

}
#endregion
