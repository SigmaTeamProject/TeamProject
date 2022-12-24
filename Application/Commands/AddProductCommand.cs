using Application.ViewModels;
using MediatR;

namespace Application.Commands
{
    public class AddProductCommand : IRequest<ProductVm>
    {
        public decimal Price { get; set; }
        public double Weight { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
