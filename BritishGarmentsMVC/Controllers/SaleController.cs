using BritishGarmentsMVC.Models;
using BritishGarmentsMVC.Service;
using Microsoft.AspNetCore.Mvc;

namespace BritishGarmentsMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController(ISaleService saleService) : ControllerBase
    {
        private readonly ISaleService _saleService = saleService;

        // GET: api/Sale
        [HttpGet("All")]
        public IActionResult GetAllSales()
        {
            var sales = _saleService.GetAllSales();
            return Ok(sales);
        }

        // POST: api/Sale
        [HttpPost("Add")]
        public IActionResult AddSale([FromBody] Sale sale)
        {
            if (ModelState.IsValid)
            {
                int saleId = _saleService.AddSale(sale);
                return Ok(new { SaleID = saleId, Message = "Sale added successfully" });
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Sale/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            await _saleService.DeleteSaleAsync(id);
            return NoContent();
        }

        // GET: api/Sale/{saleId}/details
        [HttpGet("{saleId}/details")]
        public IActionResult GetSalesDetailBySaleId(int saleId)
        {
            var saleDetails = _saleService.GetSalesDetailBySaleId(saleId);
            if (saleDetails != null)
            {
                return Ok(saleDetails);
            }
            return NotFound();
        }

        // POST: api/Sale/{saleId}/details
        [HttpPost("Details/Add")]
        public IActionResult AddSalesDetail([FromBody] IEnumerable<SalesDetail> salesDetail)
        {
            if (ModelState.IsValid)
            {
                _saleService.AddSalesDetail(salesDetail);
                return Ok(salesDetail);
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Sale/details/{saleDetailId}
        [HttpDelete("Details/{saleDetailId}")]
        public async Task<IActionResult> DeleteSaleDetail(int saleDetailId)
        {
            await _saleService.DeleteSaleDetailAsync(saleDetailId);
            return NoContent();
        }
    }
}
