using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Acme.MovieRental.Movies;
using Acme.MovieRental.Directors;
using Acme.MovieRental.Customers;
using Acme.MovieRental.Rentals;

namespace Acme.MovieRental.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class MovieRentalDbContext :
    AbpDbContext<MovieRentalDbContext>,
    ITenantManagementDbContext,
    IIdentityDbContext
{
    public DbSet<Movie> Movies { get; set; }

    public DbSet<Director> Directors { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Rental> Rentals { get; set; }


    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext and ISaasDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext and ISaasDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public MovieRentalDbContext(DbContextOptions<MovieRentalDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureTenantManagement();
        builder.ConfigureBlobStoring();

        /* Configure your own tables/entities inside here */

        builder.Entity<Director>(b =>
        {
            b.ToTable(MovieRentalConsts.DbTablePrefix + "Directors", MovieRentalConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).HasMaxLength(128);
            b.Property(x => x.Nationality).HasMaxLength(64);
        });

        builder.Entity<Movie>(b =>
        {
            b.ToTable(MovieRentalConsts.DbTablePrefix + "Movies", MovieRentalConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Title).HasMaxLength(256);
            b.HasOne(x => x.Director)
             .WithMany()
             .HasForeignKey(x => x.DirectorId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Customer>(b =>
        {
            b.ToTable(MovieRentalConsts.DbTablePrefix + "Customers", MovieRentalConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.FullName).HasMaxLength(128);
            b.Property(x => x.Email).HasMaxLength(128);
            b.Property(x => x.PhoneNumber).HasMaxLength(128);
        });

        builder.Entity<Rental>(b =>
        {
            b.ToTable(MovieRentalConsts.DbTablePrefix + "Rentals", MovieRentalConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasOne(x => x.Customer).WithMany().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.Movie).WithMany().HasForeignKey(x => x.MovieId).OnDelete(DeleteBehavior.Restrict);
        });
    }
}
