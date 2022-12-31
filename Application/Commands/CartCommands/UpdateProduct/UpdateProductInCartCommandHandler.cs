﻿using Application.Models;
using AutoMapper;
using Data;
using MediatR;
using DAL.Repositry;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.CartCommands.UpdateProduct
{
    public class UpdateProductInCartCommandHandler : IRequestHandler<UpdateProductInCartCommand, CartModel>
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<StorageItem> _storageItemRepository;
        private readonly IMapper _mapper;

        public UpdateProductInCartCommandHandler(IMapper mapper, IRepository<Cart> cartRepository, IRepository<StorageItem> storageItemRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _storageItemRepository = storageItemRepository;
        }

        public async Task<CartModel> Handle(UpdateProductInCartCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentException();

            var userId = request.UserId;
            var cart = await _cartRepository.Query()
                .Include(cart => cart.Customer)
                .Include(cart => cart.Items)
                .FirstOrDefaultAsync(cart => cart.Customer!.Id == userId, cancellationToken);

            if (!cart.Items.Any(item => item.ProductId == request.ProductId)) throw new ArgumentException();

            var storageItem = await _storageItemRepository.FirstOrDefaultAsync(item => item.ProductId == request.ProductId);

            if (storageItem == null) throw new ArgumentNullException();
            if (storageItem.Amount - request.Count < 0) throw new ArgumentException();

            var product = cart.Items.FirstOrDefault(item => item.ProductId == request.ProductId);
            product!.Amount = request.Count;

            await _cartRepository.UpdateAsync(cart);
            await _cartRepository.SaveChangesAsync();
            
            return _mapper.Map<CartModel>(cart);
        }
    }
}
