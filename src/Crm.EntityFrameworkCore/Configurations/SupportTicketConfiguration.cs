using Crm.Customers;
using Crm.Employees;
using Crm.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Configurations
{
    public class SupportTicketConfiguration : IEntityTypeConfiguration<SupportTicket>
    {
        public void Configure(EntityTypeBuilder<SupportTicket> builder)
        {
            builder.ToTable(CrmConsts.DbTablePrefix + "SupportTickets", CrmConsts.DbSchema);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Subject).HasColumnName(nameof(SupportTicket.Subject)).IsRequired();
            builder.Property(x => x.Description).HasColumnName(nameof(SupportTicket.Description)).IsRequired();
            builder.Property(x => x.TicketStatus).HasColumnName(nameof(SupportTicket.TicketStatus)).IsRequired();
            builder.Property(x => x.Priority).HasColumnName(nameof(SupportTicket.Priority)).IsRequired();
            builder.Property(x => x.LastResponseTime).HasColumnName(nameof(SupportTicket.LastResponseTime)).IsRequired();
            builder.Property(x => x.ClosedTime).HasColumnName(nameof(SupportTicket.ClosedTime)).IsRequired();

            builder.HasOne<Customer>().WithMany().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne<Employee>().WithMany().HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.NoAction);

        }

    }
}
