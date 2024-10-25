using AutoMapper;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entities.Order;

namespace E_Commerce.API.Helper
{
	public class OrderItemResolver : IValueResolver<OrderItem,OrderItemDTO,string>
	{
		private readonly IConfiguration _configuration;

		public OrderItemResolver(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
			=> !string.IsNullOrWhiteSpace(source.OrderItemProduct.PictureUrl) ? $"{_configuration["BaseUrl"]} {source.OrderItemProduct.PictureUrl}" : string.Empty;
		
	}
}
