using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace Api.Models.Product
{
    public class ProductSelectDto : BaseDto<ProductSelectDto, Entities.Product.Product, int>
    {
        public string Name { get; set; }

        public string Color { get; set; }

        public decimal Price { get; set; }
    }
}
