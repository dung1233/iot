using RESTful.Dto.Product;

namespace RESTful.Service
{
    public interface IProductService
    {

        Task<List<ProductResponse>> GetAll();
        Task<ProductResponse> GetProductAsync(string id);
        Task<Models.Product?> CreateProductAsync(CreateProduct product);
        Task<Models.Product?> UpdateProductAsync(string id, UpdateProduct updateProduct);
        Task<bool> DeleteProduct(string id);
    }
}
