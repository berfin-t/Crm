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

        var activityPermission = myGroup.AddPermission(CrmPermissions.Activities.Default, L("Permission:Activities"));
        projectPermission.AddChild(CrmPermissions.Activities.Menu, L("Permission:Menu"));
        projectPermission.AddChild(CrmPermissions.Activities.Create, L("Permission:Create"));
        projectPermission.AddChild(CrmPermissions.Activities.Edit, L("Permission:Edit"));
        projectPermission.AddChild(CrmPermissions.Activities.Delete, L("Permission:Delete"));

        var contactPermission = myGroup.AddPermission(CrmPermissions.Contacts.Default, L("Permission:Contacts"));
        contactPermission.AddChild(CrmPermissions.Contacts.Menu, L("Permission:Menu"));
        contactPermission.AddChild(CrmPermissions.Contacts.Create, L("Permission:Create"));
        contactPermission.AddChild(CrmPermissions.Contacts.Edit, L("Permission:Edit"));
        contactPermission.AddChild(CrmPermissions.Contacts.Delete, L("Permission:Delete"));

        var customerPermission = myGroup.AddPermission(CrmPermissions.Customers.Default, L("Permission:Customers"));
        customerPermission.AddChild(CrmPermissions.Customers.Menu, L("Permission:Menu"));
        customerPermission.AddChild(CrmPermissions.Customers.Create, L("Permission:Create"));
        customerPermission.AddChild(CrmPermissions.Customers.Edit, L("Permission:Edit"));
        customerPermission.AddChild(CrmPermissions.Customers.Delete, L("Permission:Delete"));

        var employeePermission = myGroup.AddPermission(CrmPermissions.Employees.Default, L("Permission:Employees"));
        employeePermission.AddChild(CrmPermissions.Employees.Menu, L("Permission:Menu"));
        employeePermission.AddChild(CrmPermissions.Employees.Create, L("Permission:Create"));
        employeePermission.AddChild(CrmPermissions.Employees.Edit, L("Permission:Edit"));
        employeePermission.AddChild(CrmPermissions.Employees.Delete, L("Permission:Delete"));

        var orderPermission = myGroup.AddPermission(CrmPermissions.Orders.Default, L("Permission:Orders"));
        orderPermission.AddChild(CrmPermissions.Orders.Menu, L("Permission:Menu"));
        orderPermission.AddChild(CrmPermissions.Orders.Create, L("Permission:Create"));
        orderPermission.AddChild(CrmPermissions.Orders.Edit, L("Permission:Edit"));
        orderPermission.AddChild(CrmPermissions.Orders.Delete, L("Permission:Delete"));

        var positionPermission = myGroup.AddPermission(CrmPermissions.Positions.Default, L("Permission:Positions"));
        positionPermission.AddChild(CrmPermissions.Positions.Menu, L("Permission:Menu"));
        positionPermission.AddChild(CrmPermissions.Positions.Create, L("Permission:Create"));
        positionPermission.AddChild(CrmPermissions.Positions.Edit, L("Permission:Edit"));
        positionPermission.AddChild(CrmPermissions.Positions.Delete, L("Permission:Delete"));
        
        var taskPermission = myGroup.AddPermission(CrmPermissions.Tasks.Default, L("Permission:Tasks"));
        taskPermission.AddChild(CrmPermissions.Tasks.Menu, L("Permission:Menu"));
        taskPermission.AddChild(CrmPermissions.Tasks.Create, L("Permission:Create"));
        taskPermission.AddChild(CrmPermissions.Tasks.Edit, L("Permission:Edit"));
        taskPermission.AddChild(CrmPermissions.Tasks.Delete, L("Permission:Delete"));

        var supportTicketPermission = myGroup.AddPermission(CrmPermissions.SupportTickets.Default, L("Permission:SupportTickets"));
        supportTicketPermission.AddChild(CrmPermissions.SupportTickets.Menu, L("Permission:Menu"));
        supportTicketPermission.AddChild(CrmPermissions.SupportTickets.Create, L("Permission:Create"));
        supportTicketPermission.AddChild(CrmPermissions.SupportTickets.Edit, L("Permission:Edit"));
        supportTicketPermission.AddChild(CrmPermissions.SupportTickets.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CrmResource>(name);
    }
}
