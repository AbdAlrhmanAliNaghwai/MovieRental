using Volo.Abp.Settings;

namespace Acme.MovieRental.Settings;

public class MovieRentalSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(MovieRentalSettings.MySetting1));
    }
}
