using Application.Dtos.Product;
using Application.Services.Contracts;
using Domain.Markers;

namespace Application.Services.Implementations;

public class ProductService : IProductService, IScopedDependency
{
    public Task<List<ProductDto>> GetAllProduct()
    {
        throw new NotImplementedException();
    }

    public Task CreateProduct(CreateProductDto dto)
    {
        return Task.CompletedTask;
    }

    public Task UpdateProduct(UpdateProductDto dto)
    {
        return Task.CompletedTask;
    }

    public Task DeleteProduct(int id)
    {
        return Task.CompletedTask;
    }
}
