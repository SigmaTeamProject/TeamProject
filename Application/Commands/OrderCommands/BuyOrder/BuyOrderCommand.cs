using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.OrderCommands.BuyOrder
{
	public class BuyOrderCommand : IRequest<OrderModel>
	{
		public int OrderId { get; set; }
		public int CartId { get; set; }
		public int CustomerId { get; set; }

	}
}
