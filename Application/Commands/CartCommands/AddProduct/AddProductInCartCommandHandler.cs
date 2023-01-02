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
                .Include(cart => cart.Items)
                .ThenInclude(items => items.Product)
                .FirstOrDefaultAsync(cart => cart.CustomerId == userId);

            if (cart == null) throw new ArgumentException();

            var storageItem = await _storageItemRepository.FirstOrDefaultAsync(item => item.ProductId == request.ProductId);

            if (storageItem == null) throw new ArgumentNullException();
            cart.Items ??= new List<CartItem>();
            if (request.Count < 0 || storageItem.Amount - request.Count < 0) throw new ArgumentException();

            if (cart.Items.Any(item => item.ProductId == request.ProductId))
            {
                var product = cart.Items.FirstOrDefault(item => item.ProductId == request.ProductId);
                product.Amount = request.Count;
            }
            else
            {
                var item = new CartItem()
                {
                    ProductId = request.ProductId,
                    Amount = request.Count
                };
                cart.Items.Add(item);
            }
            await _cartRepository.UpdateAsync(cart);
            await _cartRepository.SaveChangesAsync();

            var cartModel = new CartModel
            {
                Products = await _mapper.ProjectTo<BuyProductModel>(cart.Items.AsQueryable()).ToListAsync()
            };
            cartModel.TotalPrice = cartModel.Products.Sum(p => p.Price * p.Quantity);

            return cartModel;
        }
    }
}
