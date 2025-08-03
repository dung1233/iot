using System;
using AutoMapper;
using TTS.Dto.Product;
using TTS.Models.Products;
using TTS.Repositories.Products;

namespace TTS.Service.Products
{
    public class ServicesProduct : IServicesProduct
    {
        private readonly ProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ServicesProduct(ProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductResponsive>> GetAll()
        {
            var list = await _productRepository.GetAllAsync();
            return _mapper.Map<List<ProductResponsive>>(list);
        }
        public async Task<ProductResponsive?> Create(CreateProduct createProduct) 
        {
            if (createProduct == null)
            {
                return null;
            }
            var product = _mapper.Map<Product>(createProduct);
            var createdProduct = await _productRepository.CreateProductAsync(product);
            if (createdProduct == null)
            {
                return null;
            }   
            return _mapper.Map<ProductResponsive>(createdProduct);
        }
        public async Task<ProductResponsive?> Update(string id, UpdateProduct updateProduct)
        {
            if (updateProduct == null)
            {
                return null;

            }
            var ex = await _productRepository.UpdateProductAsync(id, _mapper.Map<Product>(updateProduct));
            if (ex == null)
            {
                return null;
            }
            return _mapper.Map<ProductResponsive>(ex);

        }
        public async Task<bool> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            return await _productRepository.DeleteProductAsync(id);
        }

    }
}
