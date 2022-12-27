using Application.Dtos;
using Application.Models;
using MediatR;

namespace Application.Queries.Product.Update;

public class CommandQuery : IRequest<UpdateProdcuctInStoregeCommandHandler>
{
    public UpdateProdcuctInStoregeCommandHandler Product { get; set; }
}