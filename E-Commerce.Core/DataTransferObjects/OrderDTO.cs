using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.DataTransferObjects
{
	public class OrderDTO
	{
        public string BasketId { get; set; }
        public string BuyerEmail { get; set; }
        public int? DeliveryMethodId { get; set; }
        public ShippingAddressDTO ShippingAddress { get; set; }
    }
}
