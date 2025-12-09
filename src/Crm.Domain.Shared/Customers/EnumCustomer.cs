using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Customers
{
    public enum EnumCustomer
    {
        Lead = 1, //Potansiyel müşteri
        Prospect = 2, //Aday müşteri
        ProposalStage = 3, //Teklif aşamasında
        Won = 4, //Kazanıldı
        Lost = 5, //Kaybedildi
        ActiveProjectCustomer = 6, //Aktif proje müşterisi

    }
}
