using Volo.Abp.Modularity;

namespace Acme.MovieRental;

[DependsOn(
    typeof(MovieRentalApplicationModule),
    typeof(MovieRentalDomainTestModule)
)]
public class MovieRentalApplicationTestModule : AbpModule
{

}
