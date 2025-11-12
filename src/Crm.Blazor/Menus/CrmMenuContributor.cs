using Crm.Localization;
using Crm.MultiTenancy;
using Crm.Permissions;
using System.Threading.Tasks;
using Volo.Abp.Identity.Blazor;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.TenantManagement.Blazor.Navigation;
using Volo.Abp.UI.Navigation;

namespace Crm.Blazor.Menus;

public class CrmMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<CrmResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                CrmMenus.Home,
                l["Menu:Dashboard"],
                "/",
                icon: "fas fa-home",
                order: 0
            )
        );

        //context.Menu.AddItem(
        //    new ApplicationMenuItem(
        //        CrmMenus.Dashboard,
        //        l["Menu:Dashboard"],
        //        icon: "fa-light fa-table-columns",
        //        url: "/dashboard",
        //        order: 1
        //    )
        //);

        context.Menu.AddItem(
            new ApplicationMenuItem(
                CrmMenus.Projects,
                l["Menu:Projects"],
                icon: "fa-regular fa-folder-open",
                url: "/projects",
                order: 1
            )
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                CrmMenus.Activities,
                l["Menu:Activities"],
                icon: "fa-regular fa-calendar-days",
                url: "/activities",
                order: 1
            )
        );

        //context.Menu.AddItem(
        //    new ApplicationMenuItem(
        //        CrmMenus.Contacts,
        //        l["Menu:Contacts"],
        //        icon: "fas fa-tasks",
        //        url: "/contacts",
        //        order: 1
        //    )
        //);

        //context.Menu.AddItem(
        //    new ApplicationMenuItem(
        //        CrmMenus.CustomerNotes,
        //        l["Menu:CustomerNotes"],
        //        icon: "fas fa-tasks",
        //        url: "/customerNotes",
        //        order: 1
        //    )
        //);

        context.Menu.AddItem(
            new ApplicationMenuItem(
                CrmMenus.Customers,
                l["Menu:Customers"],
                icon: "fa-regular fa-handshake",
                url: "/customers",
                order: 1,
                requiredPermissionName: CrmPermissions.Customers.Default
            )
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                CrmMenus.Employees,
                l["Menu:Employees"],
                icon: "fa-regular fa-circle-user",
                url: "/employees",
                order: 1
            )
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                CrmMenus.Orders,
                l["Menu:Orders"],
                icon: "fa-regular fa-hourglass-half",
                url: "/orders",
                order: 1
            )
        );

        //context.Menu.AddItem(
        //    new ApplicationMenuItem(
        //        CrmMenus.Positions,
        //        l["Menu:Positions"],
        //        icon: "fas fa-tasks",
        //        url: "/positions",
        //        order: 1
        //    )
        //);

        context.Menu.AddItem(
            new ApplicationMenuItem(
                CrmMenus.Tasks,
                l["Menu:Tasks"],
                icon: "fas fa-tasks",
                url: "/tasks",
                order: 1
            )
        );

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        //else
        //{
        //    administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        //}

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);

        return Task.CompletedTask;
    }
}
