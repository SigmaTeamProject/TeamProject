using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public decimal? Price { get; set; }
        public string? Name { get; set; }
    }
}
