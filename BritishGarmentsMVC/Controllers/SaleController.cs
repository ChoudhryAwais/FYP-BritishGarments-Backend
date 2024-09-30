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
        [HttpGet("AllSales")]
        public IActionResult GetAllSales()
        {
            var sales = _saleService.GetAllSales();
            return Ok(sales);
        }

        // POST: api/Sale
        [HttpPost("AddSale")]
        public IActionResult AddSale([FromBody] Sale sale)
        {
            if (ModelState.IsValid)
            {
                _saleService.AddSale(sale);
                return Ok(sale);
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
        [HttpPost("{saleId}/details")]
        public IActionResult AddSalesDetail([FromBody] SalesDetail salesDetail)
        {
            if (ModelState.IsValid)
            {
                _saleService.AddSalesDetail(salesDetail);
                return Ok(salesDetail);
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Sale/details/{saleDetailId}
        [HttpDelete("details/{saleDetailId}")]
        public async Task<IActionResult> DeleteSaleDetail(int saleDetailId)
        {
            await _saleService.DeleteSaleDetailAsync(saleDetailId);
            return NoContent();
        }
    }
}
