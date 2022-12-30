using Application.Models;
using AutoMapper;
using Data;
using MediatR;
using DAL.Repositry;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.CartCommands.AddProduct
{
    public class AddProductInCartCommandHandler : IRequestHandler<AddProductInCartCommand, CartModel>
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<StorageItem> _storageItemRepository;
        private readonly IMapper _mapper;

        public AddProductInCartCommandHandler(IMapper mapper, IRepository<Cart> cartRepository,
            IRepository<StorageItem> storageItemRepository)
        {
            _cartRepository = cartRepository;
            _storageItemRepository = storageItemRepository;
            _mapper = mapper;
        }
        public async Task<CartModel> Handle(AddProductInCartCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentException();
            var userId = request.UserId;
            var cart = await _cartRepository.Query()
                .Include(cart => cart.Customer)
                .Include(cart => cart.Items)
                .FirstOrDefaultAsync(cart => cart.Customer!.Id == userId);

            var storageItem = await _storageItemRepository.FirstOrDefaultAsync(item => item.ProductId == request.ProductId);

            if (storageItem == null) throw new ArgumentNullException();
            if (storageItem.Amount - request.Count < 0) throw new ArgumentException();

            var item = new StorageItem()
            {
                ProductId = request.ProductId,
                Amount = request.Count
            };

            if (cart.Items != null) cart.Items.Add(item);

            await _cartRepository.UpdateAsync(cart);
            await _cartRepository.SaveChangesAsync();

            return _mapper.Map<CartModel>(cart);
        }
    }
}
