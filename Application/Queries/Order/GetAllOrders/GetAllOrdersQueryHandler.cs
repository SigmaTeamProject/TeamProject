using Application.Models;
using AutoMapper;
using DAL.Repositry;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Order.GetAllOrders;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderPreviewModel>>
{
    private readonly IRepository<Data.Order> _repository;
    private readonly IMapper _mapper;
    public GetAllOrdersQueryHandler(IRepository<Data.Order> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderPreviewModel>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var a = await _repository.Query()
            .Where(order => order.CustomerId == request.CustomerId)
            .Include(order => order.Items)
            .ToListAsync();
        var orders = _mapper.ProjectTo<OrderPreviewModel>(
            (await _repository.Query()
                .Where(order => order.CustomerId == request.CustomerId)
                .Include(order => order.Items)
                .ToListAsync()).AsQueryable());
        return orders;
    }
}