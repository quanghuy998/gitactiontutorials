using EcommerceProject.Domain.AggregatesRoot.RoleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceProject.Infrastructure.Database.EntityTypeConfiguration
{
    internal sealed class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");

            builder.Property(p => p.Id).HasColumnName("RoleId");
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Name).HasColumnName("Name");
        }
    }
}
