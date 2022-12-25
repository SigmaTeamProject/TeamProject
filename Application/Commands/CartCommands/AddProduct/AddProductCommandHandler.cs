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
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, CartModel>
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<StorageItem> _storageItemRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AddProductCommandHandler(IMapper mapper, IRepository<Cart> cartRepository,
            IRepository<StorageItem> storageItemRepository, IConfiguration configuration)
        {
            _cartRepository = cartRepository;
            _storageItemRepository = storageItemRepository;
            _mapper = mapper;
            _config = configuration;
        }
        public async Task<CartModel> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            if (request.Count <= 0) throw new ArgumentException();

            var cart = await _cartRepository.GetById(request.CartId);

            if (cart == null) throw new ArgumentException();

            cart.Items.Add(await _storageItemRepository.GetById(request.ProductId));

            await _cartRepository.Update(cart);

            return _mapper.Map<CartModel>(cart);
        }
    }
}
