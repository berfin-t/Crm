namespace Crm.Permissions;

public static class CrmPermissions
{
    public const string GroupName = "Crm";

    public static class Projects
    {
        public const string Default = GroupName + ".Projects";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    
}
