using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Commerce.Core.Entities.Order
{
	public class Order : BaseEntity<Guid>
	{
        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public ShippingAddress ShippingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public int? DeliveryMethodId { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public decimal Subtotal { get; set; }
        public decimal Total () => Subtotal + DeliveryMethod.Price;
    }




    [JsonConverter(typeof(JsonStringEnumConverter ))]
    public enum PaymentStatus
    {
        Pending , Failed , Recieved 
    }
}
