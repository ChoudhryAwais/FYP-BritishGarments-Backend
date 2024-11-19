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
        // GET: api/Sale
        [HttpGet("User/{id}")]
        public IActionResult GetSaleByUserID(int id)
        {
            var sales = _saleService.GetSalesByUserId(id);
            return Ok(sales);
        }
        // GET: api/Sale
        [HttpGet("{saleId}")]
        public IActionResult GetSalesBySaleId(int saleId)
        {
            var sales = _saleService.GetSalesBySaleId(saleId);
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
        // DELETE: api/Sale/details/{saleDetailId}
        [HttpGet("Details/Product/{saleId}")]
        public async Task<IActionResult> GetSalesDetailsWithProductDetailBySaleIDAsync(int saleId)
        {
            var salesDetails = await _saleService.GetSalesDetailsWithProductDetailBySaleIDAsync(saleId);
            return Ok(salesDetails);
        }
        // PATCH: api/sales/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> ApproveSale(int id, [FromBody] ApproveSale updateModel)
        {
            if (updateModel == null)
            {
                return BadRequest("Invalid sale data.");
            }
            try
            {
                var sales = _saleService.GetSalesBySaleId(id);
                var sale = sales.FirstOrDefault();
                if (sale == null)
                {
                    return NotFound("Sale not found.");
                }

                if (!string.IsNullOrEmpty(updateModel.Status))
                {
                    sale.Status = updateModel.Status;
                }

                await _saleService.UpdateSaleAsync(sale);

                return NoContent(); // 204 No Content response when the update is successful
            }
            catch (Exception ex)
            {
                // Log the error as necessary
                return StatusCode(500, "An error occurred while updating the sale.");
            }
        }

    }
}
