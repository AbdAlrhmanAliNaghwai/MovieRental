using Microsoft.Extensions.Localization;
using Acme.MovieRental.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Acme.MovieRental;

[Dependency(ReplaceServices = true)]
public class MovieRentalBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<MovieRentalResource> _localizer;

    public MovieRentalBrandingProvider(IStringLocalizer<MovieRentalResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
