using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Interfaces.Services
{
	public interface IOrderService
	{
		public Task<IEnumerable<DeliveryMethod>> GetAllDeliveryMethods();
		public Task <OrderResultDTO> CreateOrderAsync (OrderDTO input);
		public Task<OrderResultDTO> GetOrderAsync(Guid id , string email);
		public Task<IEnumerable<OrderResultDTO>> GetAllOrdersAsync(string email);

	}
}
