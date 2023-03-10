using Application.Models;
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
        private readonly IRepository<CartItem> _cartItemRepository;
        private readonly IMapper _mapper;

        public UpdateProductInCartCommandHandler(IMapper mapper, IRepository<Cart> cartRepository, 
            IRepository<StorageItem> storageItemRepository, IRepository<CartItem> cartItemRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _storageItemRepository = storageItemRepository;
            _cartItemRepository = cartItemRepository;
        }

        public async Task<CartModel> Handle(UpdateProductInCartCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentException();

            var userId = request.UserId;
            var cart = await _cartRepository.Query()
                .Include(cart => cart.Customer)
                .Include(cart => cart.Items).ThenInclude(item => item.Product)
                .FirstOrDefaultAsync(cart => cart.Customer!.Id == userId, cancellationToken);

            if (cart == null) throw new ArgumentException();

            var storageItem = await _storageItemRepository.FirstOrDefaultAsync(item => item.ProductId == request.ProductId);

            if (storageItem == null) throw new ArgumentNullException();
            if (request.Count < 0 || storageItem.Amount - request.Count < 0) throw new ArgumentException();

            if(cart.Items == null) cart.Items = new List<CartItem>();

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

            var list = _cartItemRepository.Query()
                .Include(item => item.Product)
                .Where(item => cart.Items.Contains(item));
            var cartModel = new CartModel
            {
                Products = _mapper.Map<ICollection<BuyProductModel>>(list)
            };
            cartModel.TotalPrice = cartModel.Products.Select(p => p.Price * p.Amount).Sum();

            return cartModel;
        }
    }
}
