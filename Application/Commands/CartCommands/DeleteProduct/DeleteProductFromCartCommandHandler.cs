using Application.Models;
using AutoMapper;
using DAL.Repositry;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.CartCommands.DeleteProduct
{
    public class DeleteProductFromCartCommandHandler : IRequestHandler<DeleteProductFromCartCommand, CartModel>
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<StorageItem> _storageItemRepository;
        private readonly IMapper _mapper;

        public DeleteProductFromCartCommandHandler(IMapper mapper, IRepository<Cart> cartRepository, IRepository<StorageItem> storageItemRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _storageItemRepository = storageItemRepository;
        }

        public async Task<CartModel> Handle(DeleteProductFromCartCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentException();
            var userId = request.UserId;
            var cart = await _cartRepository.Query()
                .Include(cart => cart.Customer)
                .Include(cart => cart.Items)
                .FirstOrDefaultAsync(cart => cart.Customer!.Id == userId);

            if(cart.Items == null) throw new ArgumentException();
            if (!cart.Items.Any(item => item.ProductId == request.ProductId)) throw new ArgumentException();

            var itemToDelete = cart.Items.FirstOrDefault(item => item.ProductId == request.ProductId);

            if (itemToDelete != null) cart.Items.Remove(itemToDelete);

            await _cartRepository.UpdateAsync(cart);
            await _cartRepository.SaveChangesAsync();

            return _mapper.Map<CartModel>(cart);
        }
    }
}
