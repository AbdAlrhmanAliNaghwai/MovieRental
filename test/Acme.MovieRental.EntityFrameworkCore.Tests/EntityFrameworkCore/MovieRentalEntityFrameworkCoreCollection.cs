using Xunit;

namespace Acme.MovieRental.EntityFrameworkCore;

[CollectionDefinition(MovieRentalTestConsts.CollectionDefinitionName)]
public class MovieRentalEntityFrameworkCoreCollection : ICollectionFixture<MovieRentalEntityFrameworkCoreFixture>
{

}
