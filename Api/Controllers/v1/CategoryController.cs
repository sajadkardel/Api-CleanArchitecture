using Api.Models.Product;
using AutoMapper;
using Data.Contracts;
using Entities.Product;
using WebFramework.Api;

namespace Api.Controllers.v1
{
    public class CategoryController : CrudController<CategoryDto, CategorySelectDto, Category, int>
    {
        public CategoryController(IRepository<Category> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
