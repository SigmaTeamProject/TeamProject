using Application.Models;
using AutoMapper;
using Data;
using MediatR;
using Microsoft.Extensions.Configuration;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CartCommands.AddProduct
{
    public class AddProductInCartCommandHandler : IRequestHandler<AddProductInCartCommand,CartModel>
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<StorageItem> _storageItemRepository;
        private readonly IMapper _mapper;

        public AddProductInCartCommandHandler(IMapper mapper,IRepository<Cart> cartRepository,
            IRepository<StorageItem> storageItemRepository)
        {
            _cartRepository = cartRepository;
            _storageItemRepository = storageItemRepository;
            _mapper = mapper;
        }
        public async Task<CartModel> Handle(AddProductInCartCommand request,CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentException();

            var cart = await _cartRepository.GetById(request.CartId);

            cart.Items.Add(await _storageItemRepository.GetById(request.ProductId));

            await _cartRepository.Update(cart);
            await _cartRepository.SaveChangesAsync();

            return _mapper.Map<CartModel>(cart);
        }
    }
}
