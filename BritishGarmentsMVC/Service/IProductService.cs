using BritishGarmentsMVC.Models;

namespace BritishGarmentsMVC.Service
{
    public interface IProductService
    {
        IEnumerable<Product> GetProductsByCategoryId(int categoryId);
        IEnumerable<Product> GetAllProducts();
        void AddProduct(Product product);
        IEnumerable<Product> GetProductById(int productId);
        Task DeleteProductAsync(int productId);
    }
    public class ProductService(ApplicationDbContext context) : IProductService
    {
        private readonly ApplicationDbContext _context = context;
        public IEnumerable<Product> GetAllProducts()
        {
            return [.. _context.Products];
        }
        public void AddProduct(Product product)
        {
            var lastProduct = _context.Products.OrderByDescending(v => v.ProductID).FirstOrDefault();
            if (lastProduct != null)
            {
                product.ProductID = lastProduct.ProductID + 1;
            }
            else
            {
                product.ProductID = 1;
            }
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public IEnumerable<Product> GetProductById(int productId)
        {
            return [.. _context.Products.Where(p => p.ProductID == productId)];
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
        {
            return [.. _context.Products.Where(p => p.CategoryID == categoryId)];
        }
    }
}
