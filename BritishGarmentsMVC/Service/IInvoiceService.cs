using BritishGarmentsMVC.Models;

namespace BritishGarmentsMVC.Service
{
    public interface IInvoiceService
    {
        IEnumerable<Invoice> GetAllInvoices();
        int CreateInvoice(Invoice invoice);
        IEnumerable<Invoice> GetInvoiceById(int invoiceId);
    }

    public class InvoiceService(ApplicationDbContext context) : IInvoiceService
    {
        private readonly ApplicationDbContext _context = context;

        public IEnumerable<Invoice> GetAllInvoices()
        {
            return [.. _context.Invoices];
        }
        public int CreateInvoice(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            _context.SaveChanges();

            return invoice.InvoiceID;
        }

        public IEnumerable<Invoice> GetInvoiceById(int invoiceId)
        {
            return [.. _context.Invoices.Where(p => p.InvoiceID == invoiceId)];
        }

    }
}
