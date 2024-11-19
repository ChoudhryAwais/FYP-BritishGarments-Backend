using BritishGarmentsMVC.Models;
using BritishGarmentsMVC.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

        // PATCH: api/sales/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> ApproveSale(int id, [FromBody] StockUpdate updateModel)
        {
            if (updateModel == null)
            {
                return BadRequest("Invalid sale data.");
            }
            try
            {
                var products = _productService.GetProductById(id);
                var product = products.FirstOrDefault();
                if (product == null)
                {
                    return NotFound("Product not found.");
                }

                if (updateModel.Stock>0 && product.Stock>0)
                {
                    product.Stock = product.Stock - updateModel.Stock;
                }

                await _productService.UpdateProduct(product);

                return Ok(product); // 204 No Content response when the update is successful
            }
            catch (Exception ex)
            {
                // Log the error as necessary
                return StatusCode(500, "An error occurred while updating the sale.");
            }
        }
    }
}
