using Application.Models;
using MediatR;


namespace Application.Commands.StoregeCommands.UpdateProduct
{
    public class UpdateProductInStorageCommand : IRequest<StorageItemModel>
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
