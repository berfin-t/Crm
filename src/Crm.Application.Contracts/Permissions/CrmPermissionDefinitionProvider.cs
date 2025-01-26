using Crm.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Crm.Permissions;

public class CrmPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(CrmPermissions.GroupName);

        //SetProjectsPermissions(myGroup);

        var projectPermission = myGroup.AddPermission(CrmPermissions.Projects.Default, L("Permission:Projects"));
        projectPermission.AddChild(CrmPermissions.Projects.Menu, L("Permission:Menu"));
        projectPermission.AddChild(CrmPermissions.Projects.Create, L("Permission:Create"));
        projectPermission.AddChild(CrmPermissions.Projects.Edit, L("Permission:Edit"));
        projectPermission.AddChild(CrmPermissions.Projects.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CrmResource>(name);
    }
}
