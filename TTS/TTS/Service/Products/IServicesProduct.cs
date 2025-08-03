using TTS.Dto.Product;

namespace TTS.Service.Products
{
    public interface IServicesProduct
    {
       Task<List<ProductResponsive>> GetAll();
       Task<ProductResponsive> Create(CreateProduct createProduct);

        Task<ProductResponsive> Update(string id, UpdateProduct updateProduct);
        Task<bool> Delete(string id);
    }
}
