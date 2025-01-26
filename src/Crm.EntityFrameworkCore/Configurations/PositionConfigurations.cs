using Crm.Employees;
using Crm.Positions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Configurations
{
    public class PositionConfigurations: IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.ToTable(CrmConsts.DbTablePrefix + "Positions", CrmConsts.DbSchema);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnName(nameof(Position.Name)).IsRequired().HasMaxLength(128);
            builder.Property(x => x.Description).HasColumnName(nameof(Position.Description)).HasMaxLength(1024);
            builder.Property(x => x.Salary).HasColumnName(nameof(Position.Salary)).IsRequired();
            builder.Property(x => x.MinExperience).HasColumnName(nameof(Position.MinExperience)).IsRequired();
            builder.Property(x => x.MaxExperience).HasColumnName(nameof(Position.MaxExperience)).IsRequired();
        }
    }
}
