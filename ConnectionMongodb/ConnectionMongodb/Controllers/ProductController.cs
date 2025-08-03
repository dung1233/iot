using Microsoft.AspNetCore.Mvc;
using ConnectionMongodb.Repository;
using System.Threading.Tasks;
namespace ConnectionMongodb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController :ControllerBase
    {
        private readonly Repositories repositories;
        public ProductController(Repositories repositories)
        {
            this.repositories = repositories;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await repositories.GetProducsAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByid(string id)
        {
            var product = await repositories.GetProducsByIdAsync(id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Models.Producs producs)
        {
            if (producs == null)
                return BadRequest();

            producs.Id = null; // Đảm bảo Id null để MongoDB tự tạo

            await repositories.CreateProducsAsync(producs);

            return CreatedAtAction(nameof(GetProductByid), new { id = producs.Id }, producs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] Models.Producs producs)
        {
            if (producs == null)
            {
                return BadRequest("Product cannot be null");
            }
            var existiongProduct = await repositories.GetProducsByIdAsync(id);
            if (existiongProduct == null)
            {
                return NotFound();
            }   
            await repositories.UpdateProducsAsync(id, producs);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var existingProduct = await repositories.GetProducsByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            } 
            await repositories.DeleteProducsAsync(id);
            return NoContent();
        }
    }


}
