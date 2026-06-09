using Acme.MovieRental.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Acme.MovieRental.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(MovieRentalEntityFrameworkCoreModule),
    typeof(MovieRentalApplicationContractsModule)
)]
public class MovieRentalDbMigratorModule : AbpModule
{
}
