using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Customers
{
    public class CustomerConsts
    {
        public const string DefaultSorting = "{0} Name desc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Customer." : string.Empty);
        }
    }
}
