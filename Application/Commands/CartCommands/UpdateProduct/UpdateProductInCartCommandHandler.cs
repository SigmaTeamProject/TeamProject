using Application.Models;
using AutoMapper;
using Data;
using MediatR;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CartCommands.UpdateProduct
{
    public class UpdateProductInCartCommandHandler : IRequestHandler<UpdateProductInCartCommand, CartModel>
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IMapper _mapper;

        public UpdateProductInCartCommandHandler(IMapper mapper, IRepository<Cart> cartRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<CartModel> Handle(UpdateProductInCartCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentException();

            var cart = await _cartRepository.GetById(request.CartId);

            var product = cart.Items.FirstOrDefault(item => item.Product.Id == request.ProductId);
            product.Amount = request.Count;

            await _cartRepository.Update(cart);
            await _cartRepository.SaveChangesAsync();

            return _mapper.Map<CartModel>(cart);
        }
    }
}
