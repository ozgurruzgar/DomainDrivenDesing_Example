using MediatR;
using Order.Domain.Events;
using Order.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.AggregateModels.OrderModels
{
    public class Order:BaseEntity,IAggregateRoot
    {
        //İçerisinde orderları ve order adresini vs. yöneticek. Bütün order modellerinin yönetileceği en üst çıtadaki modelimiz

        public DateTime OrderDate { get; set; }
        public string Description { get; set; }
        public string userName { get; set; }
        public string OrderState { get; set; }
        public Address Address { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

        public Order(DateTime orderDate, string description, int buyerId, string orderState, Address address, ICollection<OrderItem> orderItems)
        {
            if(orderDate < DateTime.Now)
            {
                throw new Exception("OrderDate must be greater than now.");
            }
            if(address.City =="")
            {
                throw new Exception("City cannot be empty");
            }

            OrderDate = orderDate;
            Description = description;
            userName = userName;
            OrderState = orderState;
            Address = address;
            OrderItems = orderItems;

            AddDomainEvents(new OrderStartedDomainEvent(userName,this));
        }

        public void AddOrderItem(int quantity,decimal price, int productId)
        {
            OrderItem item = new(quantity, price, productId);

            OrderItems.Add(item);
        }
    }
}
