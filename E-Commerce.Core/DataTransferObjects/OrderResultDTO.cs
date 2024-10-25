using E_Commerce.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.DataTransferObjects
{
	public class OrderResultDTO
	{
		public Guid Id { get; set; }
		public string BuyerEmail { get; set; }
		public DateTime OrderDate { get; set; } = DateTime.Now;

		public ShippingAddressDTO ShippingAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public IEnumerable<OrderItemDTO> OrderItems { get; set; }
		public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
		public decimal Subtotal { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal Total { get; set; }
	}
}
