using BritishGarmentsMVC.Models;

namespace BritishGarmentsMVC.Service
{
    public interface IVendorService
    {
        IEnumerable<Vendor> GetAllVendors();
        void AddVendor(Vendor vendor);
        IEnumerable<Vendor> GetVendorById(int vendorId);
        Task DeleteVendorAsync(int vendorId);
    }

    public class VendorService(ApplicationDbContext context) : IVendorService
    {
        private readonly ApplicationDbContext _context = context;
        public IEnumerable<Vendor> GetAllVendors()
        {
            return [.. _context.Vendors];
        }
        public void AddVendor(Vendor vendor)
        {
            var lastVendor = _context.Vendors.OrderByDescending(v => v.VendorID).FirstOrDefault();

            if (lastVendor != null)
            {
                vendor.VendorID = lastVendor.VendorID + 1;
            }
            else
            {
                vendor.VendorID = 100;
            }

            _context.Vendors.Add(vendor);
            _context.SaveChanges();
        }
        public IEnumerable<Vendor> GetVendorById(int vendorId)
        {
            return [.. _context.Vendors.Where(p => p.VendorID == vendorId)];
        }
        public async Task DeleteVendorAsync(int vendorId)
        {
            var vendor = await _context.Vendors.FindAsync(vendorId);
            if (vendor != null)
            {
                _context.Vendors.Remove(vendor);
                await _context.SaveChangesAsync();
            }
        }
    }
}
