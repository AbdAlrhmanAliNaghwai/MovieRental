using Acme.MovieRental.EntityFrameworkCore;
using Xunit;

namespace Acme.MovieRental.Movies;

[Collection(MovieRentalTestConsts.CollectionDefinitionName)]
public class EfCoreMovieAppServiceTests : MovieAppServiceTests<MovieRentalEntityFrameworkCoreTestModule>
{
}