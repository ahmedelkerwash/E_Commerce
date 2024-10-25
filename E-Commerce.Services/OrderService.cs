using AutoMapper;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Entities.Order;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Core.Interfaces.Services;
using E_Commerce.Repository.Specifications;

namespace E_Commerce.Services
{
	public class OrderService : IOrderService
	{
		private readonly IBasketService _basketService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public OrderService(IBasketService basketService, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_basketService = basketService;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<OrderResultDTO> CreateOrderAsync(OrderDTO input)
		{
			// 1 - Get Basket
			var basket = await _basketService.GetBasketAsync(input.BasketId);
			if (basket is null)
				throw new Exception($"No busket with {input.BasketId} id");


			// 2 - Create Order Items list and get order items from basket items
			var orderItems = new List<OrderItemDTO>();
			foreach (var basketItem in basket.BasketItems)
			{
				var product = await _unitOfWork.Repository<Product, int>().GetAsync(basketItem.ProductId);
				if (product is null)
					continue;

				var productItem = new OrderItemProduct()
				{
					PictureUrl = product.PictureUrl,
					ProductName = product.Name,
					ProductId = product.Id
				};

				var orderItem = new OrderItem()
				{
					OrderItemProduct = productItem,
					Price = product.Price,
					Quantity = basketItem.Quantity,
				};

				var mappedItem = _mapper.Map<OrderItemDTO>(orderItem);
				orderItems.Add(mappedItem);

			}

			if (!orderItems.Any())
				throw new Exception("No basket items was found");

			// 3 - Delivery Method

			if (!input.DeliveryMethodId.HasValue)
				throw new Exception("There is no delivery method!");

			var delivery = await _unitOfWork.Repository<DeliveryMethod, int>().GetAsync(input.DeliveryMethodId.Value);

            if (delivery is null)
				throw new Exception($"No delivery method with {input.DeliveryMethodId} id");


			// 4 - Shipping address
			
			var shippingAddress = _mapper.Map<ShippingAddress>(input.ShippingAddress);

			// 5 - Subtotal

			var subTotal = orderItems.Sum(o=>o.Price*o.Quantity);

			// 6 - Map from OrderItemDTO ==> OrderItem

			var mappedItems = _mapper.Map<List<OrderItem>>(orderItems);

			var order = new Order()
			{
				BuyerEmail = input.BuyerEmail,
				ShippingAddress = shippingAddress,
				DeliveryMethod = delivery,
				OrderItems = mappedItems,
				Subtotal = subTotal
			};


			await _unitOfWork.Repository<Order,Guid>().AddAsync(order);
			return _mapper.Map<OrderResultDTO>(order);
		}

		public async Task<IEnumerable<DeliveryMethod>> GetAllDeliveryMethods()
		{
			return await _unitOfWork.Repository<DeliveryMethod,int>().GetAllAsync();
		}

		public async Task<IEnumerable<OrderResultDTO>> GetAllOrdersAsync(string email)
		{
			var specs = new OrderSpecification(email);
			var orders = await _unitOfWork.Repository<Order, Guid>().GetAllWithSpecAsync(specs);
			return _mapper.Map<IEnumerable<OrderResultDTO>>(orders);
		}

		public async Task<OrderResultDTO> GetOrderAsync(Guid id, string email)
		{
			var specs = new OrderSpecification(id,email);
			var orders = await _unitOfWork.Repository<Order, Guid>().GetWithSpecAsync(specs);
			return _mapper.Map<OrderResultDTO>(orders);
		}
	}
}
