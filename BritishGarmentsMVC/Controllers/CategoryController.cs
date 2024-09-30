using BritishGarmentsMVC.Models;
using BritishGarmentsMVC.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BritishGarmentsMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;

        // GET: api/category
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            var categories = _categoryService.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Category>> GetCategoryById(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null || !category.Any())
            {
                return NotFound();
            }
            return Ok(category);
        }


        [HttpPost("Add")]
        public ActionResult<Category> AddCategory(Category category)
        {
            _categoryService.AddCategory(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.CategoryID }, category);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = _categoryService.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            await _categoryService.DeleteCategoryAsync(id);

            return Ok(category);
        }
    }
}
