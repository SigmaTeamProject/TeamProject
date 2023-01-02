using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class StorageItemModel
    {
        public decimal Price { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProductCharacteristicModel> Characteristics { get; set; } = new List<ProductCharacteristicModel>();
        public int Amount { get; set; }
    }
}
