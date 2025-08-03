using Microsoft.AspNetCore.Mvc;
using TTS.Dto.Product;
using TTS.Service.Products;
namespace TTS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IServicesProduct _servicesProduct;
        public ProductController(IServicesProduct servicesProduct)
        {
            _servicesProduct = servicesProduct;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductResponsive>>> GetAll()
        {
            var products = await _servicesProduct.GetAll();
            if (products == null || !products.Any())
            {
                return NotFound("No products found.");
            }
            return Ok(products);
        }
        [HttpPost]
        public async Task<ActionResult<ProductResponsive>> Create([FromBody] CreateProduct createProduct)
        {
            if (createProduct == null)
            {
                return BadRequest("Product data is required.");
            }
            var createdProduct = await _servicesProduct.Create(createProduct);
            if (createdProduct == null)
            {
                return BadRequest("Failed to create product.");
            }
            return CreatedAtAction(nameof(GetAll), new { id = createdProduct.Id }, createdProduct);



        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductResponsive>> Update(string id, [FromBody] UpdateProduct updateProduct)
        {
            if (updateProduct == null)
            {
                return BadRequest("Product data is required.");
            }
            var ox = await _servicesProduct.Update(id, updateProduct);
            if (ox == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            return Ok(ox);


        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Product ID is required.");
            }
            var os = await _servicesProduct.Delete(id);
            if (!os)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            return Ok(true);

        }


    }
}
