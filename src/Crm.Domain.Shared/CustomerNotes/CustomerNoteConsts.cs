
namespace Crm.CustomerNotes
{
    public class CustomerNoteConsts
    {
        public const int MinNoteLength = 3;
        public const int MaxNoteLength = 2048;

        public const string DefaultSorting = "NoteDate desc";

        public static string GetDefaultSorting(bool withEntityName = true)
        {
            return string.Format(DefaultSorting, withEntityName ? "CustomerNote." : string.Empty);
        }
    }
}
