using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Extensions
{
    internal class InitialisedData
    {
        public static IEnumerable<Customer> Customers =>
            new List<Customer>
            {
                Customer.Create(CustomerId.Of(new Guid("eadc293f-c1fa-45e0-8165-493b765d7086")), "Abul Hasan", "abul.hasan.de@gmail.com" ),
                Customer.Create(CustomerId.Of(new Guid("f1dcdcd4-f7fb-46f0-9582-4c78389366ad")), "Novera Hasan", "novera.hasan.de@gmail.com" ),
            };

        public static IEnumerable<Product> Products =>
            new List<Product>
            {
                        Product.Create(ProductId.Of(new Guid("cb5588b0-554b-4f2e-b6e1-cdbff7ce56e2")), "IPhone 12", 996 ),
                        Product.Create(ProductId.Of(new Guid("7000a575-0083-4631-bb70-834a19c6766d")), "Sumsung 24FE", 750 ),
                        Product.Create(ProductId.Of(new Guid("253aa502-7897-4c54-8c1f-9b5906fb2bf7")), "IPhone 13 Pro Max", 1200 ),
                        Product.Create(ProductId.Of(new Guid("072c3df8-8869-4e98-9ba8-b99960807a5b")), "Sumsung Note", 1400 )
            };

        public static IEnumerable<Order> OrdersWithItems
        {
            get
            {
                var addressOne = Address.Of("Abul", "Hasan", "abul.hasan.de@gmail.com", "Mendelstr. 21", "Germany", "Bayern", "90429");
                var addressTwo = Address.Of("Novera", "Hasan", "abul.hasan.de@gmail.com", "Mendelstr. 21", "Germany", "Bayern", "90429");

                var paymentOne = Payment.Of("abul", "52907878787", "12/2025", "136", "1");
                var paymentTwo = Payment.Of("novera", "498707878787", "12/2029", "526", "2");

                var orderOne = Order.Create(
                    OrderId.Of(Guid.NewGuid()), 
                    CustomerId.Of(new Guid("eadc293f-c1fa-45e0-8165-493b765d7086")),
                    OrderName.Of("ORD_001"),
                    shippingAddress: addressOne,
                    billingAddress: addressTwo,
                    paymentOne, OrderStatus.Pending);

                orderOne.Add(ProductId.Of(new Guid("cb5588b0-554b-4f2e-b6e1-cdbff7ce56e2")), 2, 996);
                orderOne.Add(ProductId.Of(new Guid("7000a575-0083-4631-bb70-834a19c6766d")), 1, 750);

                var orderTwo = Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("f1dcdcd4-f7fb-46f0-9582-4c78389366ad")),
                    OrderName.Of("ORD_002"),
                    shippingAddress: addressTwo,
                    billingAddress: addressTwo,
                    paymentTwo, OrderStatus.Pending);

                orderTwo.Add(ProductId.Of(new Guid("253aa502-7897-4c54-8c1f-9b5906fb2bf7")), 2, 1200);
                orderTwo.Add(ProductId.Of(new Guid("072c3df8-8869-4e98-9ba8-b99960807a5b")), 1, 1400);

                return new List<Order> { orderOne, orderTwo };
            }
        }
    }
}
