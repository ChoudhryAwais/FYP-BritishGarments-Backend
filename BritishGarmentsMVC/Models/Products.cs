namespace BritishGarmentsMVC.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryID { get; set; }
        public int VendorID { get; set; }
        public string ProductImage { get; set; }


    }

    public class StockUpdate
    {
        public int Stock { get; set; }
    }

}
