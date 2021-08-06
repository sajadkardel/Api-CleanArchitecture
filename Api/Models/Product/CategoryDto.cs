using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace Api.Models.Product
{
    public class CategoryDto : BaseDto<CategoryDto, Entities.Product.Category, int>
    {
        public string Name { get; set; }
    }
}
