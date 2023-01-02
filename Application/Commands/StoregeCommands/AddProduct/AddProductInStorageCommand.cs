using Application.Models;
using MediatR;


namespace Application.Commands.StoregeCommands.AddProduct
{
    public class AddProductInStorageCommand : IRequest<StorageItemModel>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
    }
}
