using BritishGarmentsMVC.Models;

namespace BritishGarmentsMVC.Service
{
    public interface ISaleService
    {
        IEnumerable<Sale> GetAllSales();
        void AddSale(Sale sale);
        Task DeleteSaleAsync(int saleId);
        IEnumerable<SalesDetail> GetSalesDetailBySaleId(int saleId);
        void AddSalesDetail(SalesDetail saleDetail);
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

        public void AddSale(Sale sale)
        {
            var lastSale = _context.Sales.OrderByDescending(v => v.SaleID).FirstOrDefault();
            if (lastSale != null)
            {
                sale.SaleID = lastSale.SaleID + 1;
            }
            else
            {
                sale.SaleID = 1;
            }
            _context.Sales.Add(sale);
            _context.SaveChanges();
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

        public void AddSalesDetail(SalesDetail saleDetail)
        {
            var lastSaleDetail = _context.SalesDetails.OrderByDescending(v => v.SalesDetailID).FirstOrDefault();
            if (lastSaleDetail != null)
            {
                saleDetail.SalesDetailID = lastSaleDetail.SalesDetailID + 1;
            }
            else
            {
                saleDetail.SalesDetailID = 1;
            }
            _context.SalesDetails.Add(saleDetail);
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
