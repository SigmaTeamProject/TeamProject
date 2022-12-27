using Application.Commands.OrderCommands.BuyOrder;
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

namespace Application.Commands.OrderCommands.AddPaymentMethod
{
	internal class AddPaymentMethodCommandHandler : IRequestHandler<AddPaymentMethodCommand, CustomerModel>
	{
		private readonly IRepository<Customer> _customerRepository;
		private readonly IMapper _mapper;

		public async Task<CustomerModel> Handle(AddPaymentMethodCommand request, CancellationToken cancellationToken)
		{
			if (request == null)
				throw new ArgumentException();

			var customerId = request.CustomerId;

			var customer =
				await _customerRepository.Query().Include(customer => customer.Id)
				.FirstOrDefaultAsync(customer => customer.Id == customerId, cancellationToken);

			customer.PaymentConfig.PaymentType = request.PaymentMethod;

			await _customerRepository.UpdateAsync(customer);
			await _customerRepository.SaveChangesAsync();

			return _mapper.Map<CustomerModel>(customer);
		}
	}
}
