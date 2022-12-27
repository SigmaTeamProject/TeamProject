using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.OrderCommands.AddPaymentMethod
{
	internal class AddPaymentMethodCommand : IRequest<CustomerModel>
	{
		public int CustomerId { get; set; }
		public string PaymentMethod { get; set; }
	}
}
