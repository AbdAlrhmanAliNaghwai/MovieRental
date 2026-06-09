using Acme.MovieRental.Samples;
using Xunit;

namespace Acme.MovieRental.EntityFrameworkCore.Applications;

[Collection(MovieRentalTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<MovieRentalEntityFrameworkCoreTestModule>
{

}
