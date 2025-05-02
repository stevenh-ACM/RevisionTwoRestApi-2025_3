using Microsoft.EntityFrameworkCore;

using RevisionTwoApp.RestApi.Models;
using RevisionTwoApp.RestApi.Models.App;


namespace RevisionTwoApp.RestApi.Data
{
    //public class AppDbContext : IdentityDbContext<DemoUser>
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Credential> Credentials { get; set; } = default!;

        public DbSet<Address_App> Addresses { get; set; } = default!;
        public DbSet<Bill_App> Bills { get; set; } = default!;
        public DbSet<BillDetail_App> BillDetails { get; set; } = default!;
        public DbSet<Contact_App> Contacts { get; set; } = default!;
        public DbSet<Case_App> Cases { get; set; } = default!;
        public DbSet<Opportunity_App> Opportunities { get; set; } = default!;
        public DbSet<SalesOrder_App> SalesOrders { get; set; } = default!;
        public DbSet<Shipment_App> Shipments { get; set; } = default!;
        public DbSet<ShipmentDetail_App> ShipmentDetails { get; set; } = default!;

    }
}
