using Application.Models;
using MediatR;


namespace Application.Commands.StoregeCommands.UpdateProduct
{
    public class UpdateProductInStoregeCommand : IRequest<ProductModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
