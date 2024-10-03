using BritishGarmentsMVC.Models;

namespace BritishGarmentsMVC.Service
{
    public interface ISaleService
    {
        IEnumerable<Sale> GetAllSales();
        int AddSale(Sale sale);
        Task DeleteSaleAsync(int saleId);
        IEnumerable<SalesDetail> GetSalesDetailBySaleId(int saleId);
        void AddSalesDetail(IEnumerable<SalesDetail> saleDetail);
        Task DeleteSaleDetailAsync(int saleDetailId);

    }
    public class SaleService(ApplicationDbContext context) : ISaleService
    {
        private readonly ApplicationDbContext _context = context;

        //Sale services
        public IEnumerable<Sale> GetAllSales()
        {
            return [.. _context.Sales];
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

    }

}
