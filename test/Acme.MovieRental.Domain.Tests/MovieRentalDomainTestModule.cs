using Volo.Abp.Modularity;

namespace Acme.MovieRental;

[DependsOn(
    typeof(MovieRentalDomainModule),
    typeof(MovieRentalTestBaseModule)
)]
public class MovieRentalDomainTestModule : AbpModule
{

}
