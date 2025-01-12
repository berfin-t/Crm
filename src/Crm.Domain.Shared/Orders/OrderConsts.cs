using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Orders
{
    public class OrderConsts
    {
        public const string DefaultSorting = "CreationTime DESC";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Order." : string.Empty);
        }
    }
}
