using CacheWithRAM.Service;
using Microsoft.AspNetCore.Mvc;

namespace CacheWithRAM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly InMemoryCacheService _cacheService;

        public ProductController(InMemoryCacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpGet("{productID}")]
        public IActionResult GetProduct(string productID)
        {
            var cachekey = $"Product_{productID}";
            var cachedProduct = _cacheService.GetFromCache(cachekey);
            if (cachedProduct != null)
            {
                return Ok(cachedProduct);
            }
            var product = _cacheService.GetFromDataStore(cachekey);
            if (product == null)
            {
                return NotFound($"Product with ID {productID} not found.");
            }
            _cacheService.AddToCache(cachekey, product);
            return Ok(product);

        }
        [HttpDelete("{productID}")]
        public IActionResult DeleteProduct(string productID)
        {
            var cachekey = $"Product_{productID}";
            _cacheService.RemoveFromMemoryCache(cachekey);
            _cacheService.RemoveFromDataStore(cachekey);
            return NoContent();

        }



    }


}
