using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Acme.MovieRental.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class MovieRentalDbContextFactory : IDesignTimeDbContextFactory<MovieRentalDbContext>
{
    public MovieRentalDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        MovieRentalEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<MovieRentalDbContext>()
            .UseSqlite(configuration.GetConnectionString("Default"));
        
        return new MovieRentalDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Acme.MovieRental.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables();

        return builder.Build();
    }
}
