using Application.Models;
using AutoMapper;
using DAL.Repositry;
using Data;
using MediatR;
using System.Runtime.InteropServices;

namespace Application.Commands.StoregeCommands.AddProduct
{
    public class AddProductInStoregeCommandHandler : IRequestHandler<AddProductInStoregeCommand,ProductModel>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Product> _repository;

        public AddProductInStoregeCommandHandler(IMapper mapper,IRepository<Product> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ProductModel> Handle(AddProductInStoregeCommand request,CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentException();
            }

            await _repository.AddAsync(_mapper.Map<Product>(request));
            await _repository.SaveChangesAsync();

            return _mapper.Map<ProductModel>(request);


        }
    }
}
