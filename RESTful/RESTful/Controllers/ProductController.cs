using Microsoft.AspNetCore.Mvc;
using RESTful.Dto.Product;
using RESTful.Service;
namespace RESTful.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Models.Product>>> GetProductsAsync()
        {
            var products = await _productService.GetAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Product>> GetProductAsync(string id)
        {
            var product = await _productService.GetProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult<Models.Product>> CreateProductAsync([FromBody] CreateProduct createProductDto)
        {
            if (createProductDto == null)
            {
                return BadRequest("Dữ liệu sản phẩm không được để trống");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdProduct = await _productService.CreateProductAsync(createProductDto);
            if (createdProduct == null)
            {
                return BadRequest("Failed to create product");
            }

         
            // Return 201 Created with the created product
            return Created($"api/Product/{createdProduct.Id}", createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Models.Product>> UpdateProductAsync(string id, [FromBody] UpdateProduct updateProduct)
        {
            if (updateProduct == null)
            {
                return BadRequest("Product cannot be null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
           

            var updatedProduct = await _productService.UpdateProductAsync(id, updateProduct);
            if (updatedProduct == null)
            {
                return NotFound($"Product with id {id} not found");
            }
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductAsync(string id)
        {
            var product = await _productService.GetProductAsync(id);
            if (product == null)
            {
                return NotFound($"Product with id {id} not found");
            }
            var deleteResult = await _productService.DeleteProduct(id);
            if (!deleteResult)
            {
                return BadRequest("Failed to delete product");
            }
            return NoContent();
        }

    }
}
