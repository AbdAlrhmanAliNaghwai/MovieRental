using Volo.Abp.Modularity;

namespace Acme.MovieRental;

public abstract class MovieRentalApplicationTestBase<TStartupModule> : MovieRentalTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
