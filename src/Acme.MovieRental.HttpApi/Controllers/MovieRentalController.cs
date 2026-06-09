using Acme.MovieRental.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Acme.MovieRental.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class MovieRentalController : AbpControllerBase
{
    protected MovieRentalController()
    {
        LocalizationResource = typeof(MovieRentalResource);
    }
}
