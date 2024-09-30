using BritishGarmentsMVC.Models;
using System.Numerics;

namespace BritishGarmentsMVC.Service
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories();
        void AddCategory(Category category);
        IEnumerable<Category> GetCategoryById(int categoryId);
        Task DeleteCategoryAsync(int categoryId);
    }

    public class CategoryService(ApplicationDbContext context) : ICategoryService
    {
        private readonly ApplicationDbContext _context = context;
        public IEnumerable<Category> GetAllCategories()
        {
            return [.. _context.Categories];
        }
        public void AddCategory(Category category)
        {
            var lastCategory = _context.Categories.OrderByDescending(v => v.CategoryID).FirstOrDefault();


            if (lastCategory != null)
            {
                category.CategoryID = lastCategory.CategoryID + 1;
            }
            else
            {
                category.CategoryID = 1;
            }


            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        public IEnumerable<Category> GetCategoryById(int categoryId)
        {
            return [.. _context.Categories.Where(p => p.CategoryID == categoryId)];
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

    }

}
