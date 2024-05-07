using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.Order;

namespace Talabat.Core.Specification.EntitySpecification.Orders
{
    public class OrderSpecification : BaseSpecification<Order>
    {
        public OrderSpecification(string buyerEmail) : base(o => o.BayerEmail == buyerEmail)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
            AddOrderByDes(o => o.OrderDate);
        }
        public OrderSpecification(string buyerEmail, int orderId) : base(o => o.BayerEmail == buyerEmail && o.Id == orderId)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
        }
    }
}
