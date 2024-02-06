using Api.Models.Product;
using AutoMapper;
using Domain.Entities.Product;
using Domain.Repositories.Contracts;
using Infrastructure.Api;

namespace Api.Controllers.v1;

public class ProductController : CrudController<ProductDto, ProductSelectDto, Product, int>
{
    public ProductController(IRepository<Product> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
