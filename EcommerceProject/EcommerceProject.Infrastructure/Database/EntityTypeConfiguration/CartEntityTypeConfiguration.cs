using EcommerceProject.Domain.AggregatesRoot.CartAggregate;
using EcommerceProject.Domain.AggregatesRoot.ProductAggregate;
using EcommerceProject.Domain.AggregatesRoot.UserAggregate;
using EcommerceProject.Domain.SharedKermel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceProject.Infrastructure.Database.EntityTypeConfiguration
{
    internal sealed class CartEntityTypeConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Cart");

            builder.Property(p => p.Id).HasColumnName("CartId");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.UserId).HasColumnName("UserId");
            builder.HasOne<User>().WithOne();

            builder.OwnsOne<MoneyValue>(own => own.Value, value => {
                value.Property(p => p.Currency).HasColumnName("Currency");
                value.Property(p => p.Value).HasColumnName("Value").HasColumnType("decimal(12,8)");
            });

            builder.OwnsMany<CartProduct>(own => own.CartProducts, od => {
                od.WithOwner().HasForeignKey("CartId");
                od.ToTable("CartProduct");

                od.Property(p => p.Id).HasColumnName("CartProductId");
                od.HasKey(k => k.Id);
                od.Property<int>("CartId").HasColumnName("CartId").IsRequired();
                od.Property(p => p.ProductId).HasColumnName("ProductId").IsRequired();
                od.HasOne<Product>().WithMany();
                od.OwnsOne<MoneyValue>(own => own.Price, value => {
                    value.Property(p => p.Currency).HasColumnName("Currency");
                    value.Property(p => p.Value).HasColumnName("Value").HasColumnType("decimal(12,8)");
                });
                od.Property(p => p.Quantity).HasColumnName("Quantity");

                od.HasIndex("CartId", "ProductId").IsUnique();
            });
        }
    }
}
