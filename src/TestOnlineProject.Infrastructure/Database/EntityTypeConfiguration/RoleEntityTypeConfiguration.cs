using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOnlineProject.Domain.Aggregates.RoleAggregate;

namespace TestOnlineProject.Infrastructure.Database.EntityTypeConfiguration
{
    internal sealed class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).HasColumnName("Name");
        }
    }
}
