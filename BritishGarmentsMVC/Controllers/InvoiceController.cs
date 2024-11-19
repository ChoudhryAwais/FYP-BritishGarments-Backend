using BritishGarmentsMVC.Models;
using BritishGarmentsMVC.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace BritishGarmentsMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController(IInvoiceService invoiceService) : ControllerBase
    {
        private readonly IInvoiceService _invoiceService = invoiceService;


        // GET: api/Invoices
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Vendor>> GetInvoices()
        {
            var invoices = _invoiceService.GetAllInvoices();
            return Ok(invoices);
        }


        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Category>> GetInvoiceById(int id)
        {
            var invoice = _invoiceService.GetInvoiceById(id);
            if (invoice == null || !invoice.Any())
            {
                return NotFound();
            }
            return Ok(invoice);
        }

        // GET: api/Invoices
        [HttpPost("Add")]
        public ActionResult<Invoice> AddVendor(Invoice invoice)
        {
            _invoiceService.CreateInvoice(invoice);
            return CreatedAtAction(nameof(GetInvoiceById), new { id = invoice.InvoiceID }, invoice);
        }

    }
}
