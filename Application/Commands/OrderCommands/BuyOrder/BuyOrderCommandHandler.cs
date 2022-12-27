using Application.Models;
using AutoMapper;
using DAL.Repositry;
using Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.OrderCommands.BuyOrder
{
	public class BuyOrderCommandHandler : IRequestHandler<BuyOrderCommand, OrderModel>
	{
		private readonly IRepository<Order> _orderRepository;
		private readonly IRepository<StorageItem> _storageItemRepository;
		private readonly IMapper _mapper;

		public async Task<OrderModel> Handle(BuyOrderCommand request, CancellationToken cancellationToken)
		{
			if (request == null) 
				throw new ArgumentException();

			var orderId = request.OrderId;

			var order =
				await _orderRepository.Query().Include(order => order.Customer)
				.FirstOrDefaultAsync(order => order.Customer.Id == orderId, cancellationToken);

			order.OrderItems.Add(await _storageItemRepository.GetByIdAsync(request.CartId));

			await _orderRepository.UpdateAsync(order);
			await _orderRepository.SaveChangesAsync();

			return _mapper.Map<OrderModel>(order);
		}
	}
}
