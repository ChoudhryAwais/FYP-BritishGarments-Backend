namespace BritishGarmentsMVC.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; } // Primary Key

        public int UserID { get; set; } // Foreign Key to Users table

        public int SaleID { get; set; } // Foreign Key to Sales table

        public int TotalAmount { get; set; } // Total amount of the invoice (stored as integer in cents)

        public DateTime InvoiceDate { get; set; } = DateTime.Now; // Default value set to current time
    }
}
