using Application.Models;
using MediatR;

namespace Application.Queries.Storage.GetAllProducts
{
    public class GetAllStorageItemsQuery : IRequest<IEnumerable<StorageItemModel>>
    {

    }
}
