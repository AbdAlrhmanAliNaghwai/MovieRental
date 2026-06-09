using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Acme.MovieRental.Data;
using Volo.Abp.DependencyInjection;

namespace Acme.MovieRental.EntityFrameworkCore;

public class EntityFrameworkCoreMovieRentalDbSchemaMigrator
    : IMovieRentalDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreMovieRentalDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the MovieRentalDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<MovieRentalDbContext>()
            .Database
            .MigrateAsync();
    }
}
