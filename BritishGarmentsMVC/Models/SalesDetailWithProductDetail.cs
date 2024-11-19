namespace BritishGarmentsMVC.Models
{
    public class SalesDetailWithProductDetail
    {
        public int SalesDetailID { get; set; }
        public int SaleID { get; set; }
        public int ProductID { get; set; }
        public string? ProductName { get; set; }  // ProductName from Products table
        public string? ProductImage { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int TotalPrice { get; set; }
    }
}
