using EcommerceProject.Domain.AggregatesRoot.OrderAggregate;
using EcommerceProject.Domain.AggregatesRoot.ProductAggregate;
using EcommerceProject.Domain.AggregatesRoot.UserAggregate;
using EcommerceProject.Domain.SharedKermel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EcommerceProject.Infrastructure.Database.EntityTypeConfiguration
{
    internal sealed class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.Property(p => p.Id).HasColumnName("OrderId");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.UserId).HasColumnName("UserId");
            builder.HasOne<User>().WithMany();

            builder.Property(p => p.CreateDate).HasColumnName("CreateDate");
            builder.Property(p => p.ShippingAddress).HasColumnName("ShippingAddress");
            builder.Property(p => p.ShippingPhoneNumber).HasColumnName("ShippingPhoneNumber");
            builder.Property(p => p.OrderStatus).HasColumnName("OrderStatus").HasConversion(new EnumToNumberConverter<OrderStatus, byte>());
            builder.OwnsOne<MoneyValue>(own => own.Value, value => {
                value.Property(p => p.Currency).HasColumnName("Currency");
                value.Property(p => p.Value).HasColumnName("Value").HasColumnType("decimal(12,8)");
            });

            builder.OwnsMany<OrderProduct>(own => own.OrderProducts, od => {
                od.WithOwner().HasForeignKey("OrderId");
                od.ToTable("OrderProduct");

                od.Property(p => p.Id).HasColumnName("OrderProductId");
                od.HasKey(k => k.Id);
                od.Property<int>("OrderId").HasColumnName("OrderId").IsRequired();
                od.Property(p => p.ProductId).HasColumnName("ProductId").IsRequired();
                od.HasOne<Product>().WithMany();

                od.Property(p => p.Quantity).HasColumnName("Quantity");
                od.OwnsOne<MoneyValue>(own => own.Price, value => {
                    value.Property(p => p.Currency).HasColumnName("Currency");
                    value.Property(p => p.Value).HasColumnName("Value").HasColumnType("decimal(12,8)");
                });

                od.HasIndex("OrderId", "ProductId").IsUnique();
            });
        }
    }
}
