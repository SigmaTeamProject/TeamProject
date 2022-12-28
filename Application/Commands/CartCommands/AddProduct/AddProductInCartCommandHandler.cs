using System.Data.Entity;
using Application.Models;
using AutoMapper;
using Data;
using MediatR;
using DAL.Repositry;

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
                .FirstOrDefaultAsync(cart => cart.Customer!.Id == userId, cancellationToken);

            cart.Items.Add(await _storageItemRepository.GetByIdAsync(request.ProductId));

            await _cartRepository.UpdateAsync(cart);
            await _cartRepository.SaveChangesAsync();

            return _mapper.Map<CartModel>(cart);
        }
    }
}
