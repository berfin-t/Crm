using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Activities
{
    public static class ActivityConsts
    {
        private const string DefaultSorting = "Date DESC";
        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Activity." : string.Empty);
        }
    }
}
