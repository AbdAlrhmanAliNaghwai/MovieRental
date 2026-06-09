using Acme.MovieRental.Localization;
using Volo.Abp.Application.Services;

namespace Acme.MovieRental;

/* Inherit your application services from this class.
 */
public abstract class MovieRentalAppService : ApplicationService
{
    protected MovieRentalAppService()
    {
        LocalizationResource = typeof(MovieRentalResource);
    }
}
