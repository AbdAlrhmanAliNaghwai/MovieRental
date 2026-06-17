using Acme.MovieRental.EntityFrameworkCore;
using Xunit;

namespace Acme.MovieRental.Rentals;

[Collection(MovieRentalTestConsts.CollectionDefinitionName)]
public class EfCoreRentalAppServiceTests : RentalAppServiceTests<MovieRentalEntityFrameworkCoreTestModule>
{
}