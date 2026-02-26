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

            builder.Property(x => x.Subject).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.TicketStatus).IsRequired();
            builder.Property(x => x.Priority).IsRequired(false);
            builder.Property(x => x.LastResponseTime).IsRequired(false);
            builder.Property(x => x.ClosedTime).IsRequired(false);

            builder.HasOne<Customer>()
                   .WithMany()
                   .HasForeignKey(x => x.CustomerId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<Employee>()
                   .WithMany()
                   .HasForeignKey(x => x.EmployeeId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.NoAction);
        }


    }
}
