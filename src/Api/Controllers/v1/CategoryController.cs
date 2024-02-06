using Api.Models.Product;
using AutoMapper;
using Domain.Entities.Product;
using Domain.Repositories.Contracts;
using Infrastructure.Api;

namespace Api.Controllers.v1;

public class CategoryController : CrudController<CategoryDto, CategorySelectDto, Category, int>
{
    public CategoryController(IRepository<Category> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
