using Acme.MovieRental.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Acme.MovieRental.Permissions;

public class MovieRentalPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MovieRentalPermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(MovieRentalPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MovieRentalResource>(name);
    }
}
