using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class UpdateProductInCartDto
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
