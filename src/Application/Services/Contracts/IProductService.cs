using Application.Dtos.Product;

namespace Application.Services.Contracts;

public interface IProductService
{
    public Task<List<ProductDto>> GetAllProduct();
    public Task CreateProduct(CreateProductDto dto);
    public Task UpdateProduct(UpdateProductDto dto);
    public Task DeleteProduct(int id);
}
