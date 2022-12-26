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
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, CartModel>
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<StorageItem> _storageItemRepository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IMapper mapper, IRepository<Cart> cartRepository,
            IRepository<StorageItem> storageItemRepository)
        {
            _cartRepository = cartRepository;
            _storageItemRepository = storageItemRepository;
            _mapper = mapper;
        }

        public async Task<CartModel> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentException();

            var cart = await _cartRepository.GetById(request.CartId);

            var product = cart.Items.FirstOrDefault(await _storageItemRepository.GetById(request.ProductId));
            product.Amount = request.Count;

            await _cartRepository.Update(cart);
            await _cartRepository.SaveChangesAsync();

            return _mapper.Map<CartModel>(cart);
        }
    }
}
