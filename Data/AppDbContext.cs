using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using RevisionTwoApp.RestApi.Models;
using RevisionTwoApp.RestApi.Models.App;

namespace RevisionTwoApp.RestApi.Data;

#region AppDbContext
/// <summary>
/// Represents the application's database context, providing access to the database tables and entities.
/// </summary>
public class AppDbContext: IdentityDbContext<DemoUser>
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
    /// <param name="builder">The model builder used to configure the database schema.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // Add your customizations after calling base.OnModelCreating(builder);
        base.OnModelCreating(builder);
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
