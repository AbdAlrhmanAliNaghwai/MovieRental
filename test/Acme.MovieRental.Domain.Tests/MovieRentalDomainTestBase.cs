using Volo.Abp.Modularity;

namespace Acme.MovieRental;

/* Inherit from this class for your domain layer tests. */
public abstract class MovieRentalDomainTestBase<TStartupModule> : MovieRentalTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
