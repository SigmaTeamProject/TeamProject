using Application.Models;
using AutoMapper;
using DAL.Repositry;
using Data;
using MediatR;


namespace Application.Commands.StoregeCommands.UpdateProduct
{
    public class UpdateProdcuctInStoregeCommandHandler : IRequestHandler<UpdateProductInStoregeCommand,ProductModel>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public UpdateProdcuctInStoregeCommandHandler(IRepository<Product> productRepository,IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductModel> Handle(UpdateProductInStoregeCommand request,CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentException();
            }

            var product = await _productRepository.GetByIdAsync(request.Id);

            product.Name = request.Name;
            product.Price = request.Price;


            await _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();
            return _mapper.Map<ProductModel>(product);

        }
    }
}
