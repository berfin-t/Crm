
namespace Crm.Projects
{
    public static class ProjectConsts
    {
        public const int MinNameLength = 3;
        public const int MaxNameLength = 128;

        public const int MaxDescriptionLength = 2048;
        public const int MinDescriptionLength = 2048;

        public const string DefaultSorting = "{0}Name desc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Project." : string.Empty);
        }
    }
}
