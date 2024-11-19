using BritishGarmentsMVC.Models;

namespace BritishGarmentsMVC.Service
{
    public interface ISaleService
    {
        IEnumerable<Sale> GetAllSales();
        IEnumerable<Sale> GetSalesBySaleId(int saleId);
        int AddSale(Sale sale);
        Task DeleteSaleAsync(int saleId);
        IEnumerable<SalesDetail> GetSalesDetailBySaleId(int saleId);
        void AddSalesDetail(IEnumerable<SalesDetail> saleDetail);
        Task DeleteSaleDetailAsync(int saleDetailId);
        IEnumerable<Sale> GetSalesByUserId(int userId);
        Task<List<SalesDetailWithProductDetail>> GetSalesDetailsWithProductDetail();
        Task<List<SalesDetailWithProductDetail>> GetSalesDetailsWithProductDetailBySaleIDAsync(int saleID);
        Task UpdateSaleAsync(Sale sale);


    }
    public class SaleService(ApplicationDbContext context) : ISaleService
    {
        private readonly ApplicationDbContext _context = context;
        //Sale services
        public IEnumerable<Sale> GetAllSales()
        {
            return [.. _context.Sales];
        }
        public IEnumerable<Sale> GetSalesBySaleId(int saleId)
        {
            return [.. _context.Sales.Where(p => p.SaleID == saleId)];
        }
        public int AddSale(Sale sale)
        {
            _context.Sales.Add(sale);
            _context.SaveChanges();

            return sale.SaleID;
        }
        public async Task DeleteSaleAsync(int saleId)
        {
            var sale = await _context.Sales.FindAsync(saleId);
            if (sale != null)
            {
                _context.Sales.Remove(sale);
                await _context.SaveChangesAsync();
            }
        }
        //Sale detail services
        public IEnumerable<SalesDetail> GetSalesDetailBySaleId(int saleId)
        {
            return [.. _context.SalesDetails.Where(p => p.SaleID == saleId)];
        }
        public void AddSalesDetail(IEnumerable<SalesDetail> saleDetails)
        {
            foreach (var saleDetail in saleDetails)
            {
                _context.SalesDetails.Add(saleDetail);
            }

            _context.SaveChanges();
        }
        public async Task DeleteSaleDetailAsync(int saleDetailId)
        {
            var saleDetail = await _context.SalesDetails.FindAsync(saleDetailId);
            if (saleDetail != null)
            {
                _context.SalesDetails.Remove(saleDetail);
                await _context.SaveChangesAsync();
            }
        }
        public IEnumerable<Sale> GetSalesByUserId(int userId)
        {
            return [.. _context.Sales.Where(p => p.UserID == userId)];
        }
        public async Task<List<SalesDetailWithProductDetail>> GetSalesDetailsWithProductDetail()
        {
            return await _context.GetSalesDetailsWithProductDetailAsync();
        }
        // Method to filter sales details by SaleID
        public async Task<List<SalesDetailWithProductDetail>> GetSalesDetailsWithProductDetailBySaleIDAsync(int saleID)
        {
            var allSalesDetails = await GetSalesDetailsWithProductDetail();
            return allSalesDetails.Where(sd => sd.SaleID == saleID).ToList();
        }
        public async Task UpdateSaleAsync(Sale sale)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync();
        }
    }

}
