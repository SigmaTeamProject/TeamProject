using Application.Models;
using AutoMapper;
using Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositry;

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

            var userId = request.UserId;
            var cart = await _cartRepository.Query()
                .Include(cart => cart.Customer)
                .FirstOrDefaultAsync(cart => cart.Customer!.Id == userId, cancellationToken);

            var product = cart.Items.FirstOrDefault(item => item.Product.Id == request.ProductId);
            product.Amount = request.Count;

            await _cartRepository.UpdateAsync(cart);
            await _cartRepository.SaveChangesAsync();

            return _mapper.Map<CartModel>(cart);
        }
    }
}
