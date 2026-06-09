using Acme.MovieRental.Samples;
using Xunit;

namespace Acme.MovieRental.EntityFrameworkCore.Domains;

[Collection(MovieRentalTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<MovieRentalEntityFrameworkCoreTestModule>
{

}
