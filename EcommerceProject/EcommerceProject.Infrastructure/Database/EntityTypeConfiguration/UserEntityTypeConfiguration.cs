using EcommerceProject.Domain.AggregatesRoot.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EcommerceProject.Infrastructure.Database.EntityTypeConfiguration
{
    internal sealed class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.Property(p => p.Id).HasColumnName("UserId");
            builder.HasKey(k => k.Id);
            builder.HasOne(p => p.Role).WithMany().HasForeignKey("RoleId");

            builder.Property(p => p.UserName).HasColumnName("UserName");
            builder.Property(p => p.Password).HasColumnName("Password");
            builder.Property(p => p.Name).HasColumnName("Name");
            builder.Property(p => p.Email).HasColumnName("Email");
        }
    }
}
