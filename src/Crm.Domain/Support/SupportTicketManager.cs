using Crm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;

namespace Crm.Support
{
    public class SupportTicketManager(ISupportTicketRepository supportTicketRepository): DomainService
    {
        //#region Create by Customer
        //public virtual async Task<SupportTicket> CreateByCustomerAsync(string subject, string description, Guid customerId)
        //{
            //var supportTicket = new SupportTicket(
            //    GuidGenerator.Create(),
            //    customerId,
            //    null,
            //    subject,
            //    description,                
            //    null,
            //    null,
            //    null,
            //    null
            //);

            //return await supportTicketRepository.InsertAsync(supportTicket);
        //}
        //#endregion
    }
}
