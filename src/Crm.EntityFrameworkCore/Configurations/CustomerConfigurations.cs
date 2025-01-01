using Crm.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Configurations
{
    public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable(CrmConsts.DbTablePrefix + "Customers", CrmConsts.DbSchema);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnName(nameof(Customer.Name)).IsRequired();
            builder.Property(x => x.Surname).HasColumnName(nameof(Customer.Surname)).IsRequired();
            builder.Property(x => x.Email).HasColumnName(nameof(Customer.Email)).IsRequired();
            builder.Property(x => x.Address).HasColumnName(nameof(Customer.Address));
            builder.Property(x => x.Phone).HasColumnName(nameof(Customer.Phone));
            builder.Property(x => x.CompanyName).HasColumnName(nameof(Customer.CompanyName));
        }
    }
}
