using AutoMapper;
using RESTful.Dto.Product;
using RESTful.Models;
using RESTful.Repositories;

namespace RESTful.Service
{
    //pattern Dependency Injection
    public class ProductService : IProductService
    {
        private readonly ProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(ProductRepository productRepository,IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<List<ProductResponse>> GetAll()
        {
           var list = await _productRepository.GetAllAsync();
            return _mapper.Map<List<ProductResponse>>(list);
        }

        public async Task<ProductResponse> GetProductAsync(string id)
        {

            var oo = await _productRepository.GetProductAsync(id);
            return _mapper.Map<ProductResponse>(oo);
        }
        public async Task<Product?> CreateProductAsync(CreateProduct createProduct)
        {
            if (createProduct == null)
            {
                return null;
            }
            var product = _mapper.Map<Product>(createProduct);


            return await _productRepository.CreateProductAsync(product);
        }

        public async Task<Product?> UpdateProductAsync(string id, UpdateProduct updateProduct)
        {
            if (updateProduct == null)
            {
                return null;
            }
            var existingProduct = await _productRepository.GetProductAsync(id);
            if (existingProduct == null)
            {
                return null; // Product not found
            }
        
            _mapper.Map(updateProduct, existingProduct); // Map updated properties

            return await _productRepository.UpdateProductAsync(id, existingProduct);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            return await _productRepository.DeleteProductAsync(id);
        }
    }    
}
