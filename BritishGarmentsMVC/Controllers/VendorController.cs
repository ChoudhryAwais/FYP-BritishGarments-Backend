using BritishGarmentsMVC.Models;
using BritishGarmentsMVC.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BritishGarmentsMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController(IVendorService vendorService) : ControllerBase
    {
        private readonly IVendorService _vendorService = vendorService;


        // GET: api/vendors
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Vendor>> GetCategories()
        {
            var vendors = _vendorService.GetAllVendors();
            return Ok(vendors);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Vendor>> GetVendorById(int id)
        {
            var vendor = _vendorService.GetVendorById(id);
            if (vendor == null || !vendor.Any())
            {
                return NotFound();
            }
            return Ok(vendor);
        }


        [HttpPost("Add")]
        public ActionResult<Vendor> AddVendor(Vendor vendor)
        {
            _vendorService.AddVendor(vendor);
            return CreatedAtAction(nameof(GetVendorById), new { id = vendor.VendorID }, vendor);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            var vendor = _vendorService.GetVendorById(id);

            if (vendor == null)
            {
                return NotFound();
            }

            await _vendorService.DeleteVendorAsync(id);

            return Ok(vendor);
        }
    }

}

