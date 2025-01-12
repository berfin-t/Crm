
namespace Crm.Contacts
{
    public class ContactConsts
    {
        public const string DefaultSorting = "{0}Type desc";
        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Contact." : string.Empty);
        }
    }
}
