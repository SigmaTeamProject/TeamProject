﻿using Application.Models;
using MediatR;


namespace Application.Commands.StoregeCommands.AddProduct
{
    public class AddProductInStoregeCommand : IRequest<ProductModel>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }

    }
}