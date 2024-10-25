﻿using E_Commerce.Core.Entities.Order;

namespace E_Commerce.Core.DataTransferObjects
{
	public class OrderItemDTO
	{
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string PictureUrl { get; set; }
	}
}