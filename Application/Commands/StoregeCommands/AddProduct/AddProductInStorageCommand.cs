using Application.Models;
using MediatR;


namespace Application.Commands.StoregeCommands.AddProduct
{
    public class AddProductInStorageCommand : IRequest<StorageItemModel>
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
