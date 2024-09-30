using BritishGarmentsMVC.Models;
using BritishGarmentsMVC.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BritishGarmentsMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Product>> GetProductById(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null || !product.Any())
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost("Add")]
        public ActionResult<Product> AddProduct(Product product)
        {
            _productService.AddProduct(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.CategoryID }, product);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var product = _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            await _productService.DeleteProductAsync(id);

            return Ok(product);
        }

        // GET api/product/category/{categoryId}
        [HttpGet("Category/{categoryId}")]
        public ActionResult<IEnumerable<Product>> GetProductsByCategoryId(int categoryId)
        {
            var products = _productService.GetProductsByCategoryId(categoryId);
            if (products == null || !products.Any())
            {
                return NotFound();
            }
            return Ok(products);
        }
    }
}
