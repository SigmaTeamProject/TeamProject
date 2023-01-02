using Application.Models;
using MediatR;

namespace Application.Queries.Storage.GetProductById
{
    public class GetStorageItemQuery : IRequest<StorageItemModel>
    {
        public int Id { get; set; }
    }
}
