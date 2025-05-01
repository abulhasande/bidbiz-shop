using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).HasConversion(
                              orderId => orderId.Value,
                              dbId => OrderId.Of(dbId));

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();

            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId);

            builder.ComplexProperty(
                    o => o.OrderName, nameBuilder =>
                    {
                        nameBuilder.Property(n => n.Value)
                                   .HasColumnName(nameof(Order.OrderName))
                                   .HasMaxLength(100)
                                   .IsRequired();
                    });

            builder.ComplexProperty(
                o => o.ShippingAddress, addressbuilder =>
                {
                    addressbuilder.Property( a => a.FirstName).HasMaxLength(100).IsRequired();
                    addressbuilder.Property(a => a.LastName).HasMaxLength(100).IsRequired();
                    addressbuilder.Property(a => a.EmailAddress).HasMaxLength(100).IsRequired();
                    addressbuilder.Property(a => a.AddressLine).HasMaxLength(100).IsRequired();
                    addressbuilder.Property(a => a.Country).HasMaxLength(100).IsRequired();
                    addressbuilder.Property(a => a.State).HasMaxLength(100).IsRequired();
                    addressbuilder.Property(a => a.ZipCode).HasMaxLength(100).IsRequired();
                });

            builder.ComplexProperty(
                o => o.BillingAddress, addressbuilder =>
                {
                    addressbuilder.Property(a => a.FirstName).HasMaxLength(100).IsRequired();
                    addressbuilder.Property(a => a.LastName).HasMaxLength(100).IsRequired();
                    addressbuilder.Property(a => a.EmailAddress).HasMaxLength(100).IsRequired();
                    addressbuilder.Property(a => a.AddressLine).HasMaxLength(100).IsRequired();
                    addressbuilder.Property(a => a.Country).HasMaxLength(100).IsRequired();
                    addressbuilder.Property(a => a.State).HasMaxLength(100).IsRequired();
                    addressbuilder.Property(a => a.ZipCode).HasMaxLength(100).IsRequired();
                });

            builder.ComplexProperty(
                o => o.Payment, paymentbuilder =>
                {
                    paymentbuilder.Property(p => p.CardName).HasMaxLength(100);
                    paymentbuilder.Property(p => p.CardNumber).HasMaxLength(100).IsRequired();
                    paymentbuilder.Property(p => p.Expiration).HasMaxLength(100);
                    paymentbuilder.Property(p => p.CVV).HasMaxLength(3).IsRequired();
                    paymentbuilder.Property(p => p.PaymentMethod).HasMaxLength(3).IsRequired();
                });

            builder.Property(o => o.Status)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(
                    s => s.ToString(),
                    dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

            builder.Property(o => o.ToatlPrice);

        }
    }
}
