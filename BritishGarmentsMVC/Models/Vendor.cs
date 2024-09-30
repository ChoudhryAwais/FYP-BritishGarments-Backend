namespace BritishGarmentsMVC.Models
{
    public class Vendor
    {
        public int VendorID { get; set; }          
        public string VendorName { get; set; }    
        public string ContactInfo { get; set; } 
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}
