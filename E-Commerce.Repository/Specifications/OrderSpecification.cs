using E_Commerce.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Specifications
{
	public class OrderSpecification : BaseSpecifications<Order>
	{
        public OrderSpecification(string email) : base(order => order.BuyerEmail == email)
        {
            IncludeExpressions.Add(o => o.DeliveryMethod);
            IncludeExpressions.Add(o => o.OrderItems);
            OrderByDescExp = o => o.OrderDate;
		}

		public OrderSpecification(Guid id , string email) : base(order => order.BuyerEmail == email && order.Id == id)
		{
			IncludeExpressions.Add(o => o.DeliveryMethod);
			IncludeExpressions.Add(o => o.OrderItems);
			OrderByDescExp = o => o.OrderDate;
		}
	}
}
