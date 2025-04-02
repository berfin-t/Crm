using Crm.Customers;
using Crm.Orders;
using Crm.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(CrmConsts.DbTablePrefix + "Orders", CrmConsts.DbSchema);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Status).HasColumnName(nameof(Order.Status)).IsRequired();
            builder.Property(x => x.OrderDate).HasColumnName(nameof(Order.OrderDate)).IsRequired();
            builder.Property(x => x.DeliveryDate).HasColumnName(nameof(Order.DeliveryDate));
            builder.Property(x => x.TotalAmount).HasColumnName(nameof(Order.TotalAmount)).IsRequired();
            builder.Property(x => x.OrderCode).HasColumnName(nameof(Order.OrderCode)).IsRequired();

            builder.HasOne<Customer>().WithMany().IsRequired().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne<Project>().WithMany().IsRequired().HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
