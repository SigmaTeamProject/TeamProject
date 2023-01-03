using Application.Mapping;
using Application.Models;
using AutoMapper;
using DAL.Repositry;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Cart;

public class GetCartQueryHandler : IRequestHandler<GetCartQuery, CartModel>
{
    private readonly IRepository<Data.Cart> _cartRepository;
    private readonly IMapper _mapper;

    public GetCartQueryHandler(IRepository<Data.Cart> cartRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    public async Task<CartModel> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.Query()
            .Include(cart => cart.Items)!
            .ThenInclude(item => item.Product)
            .FirstOrDefaultAsync(cart => cart.CustomerId == request.CustomerId, cancellationToken: cancellationToken);
        var cartModel = new CartModel
        {
            Products = _mapper.Map<ICollection<BuyProductModel>>(cart!.Items)
        };
        cartModel.TotalPrice = cartModel.Products.Sum(model => model.Price * model.Amount);
        return cartModel;
    }
}