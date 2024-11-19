namespace BritishGarmentsMVC.Models
{
    public class Sale
    {
        public int SaleID { get; set; } // Primary Key

        public int UserID { get; set; } // Foreign Key to Users table

        public DateTime SaleDate { get; set; } = DateTime.Now; // Default value set to current time

        public int TotalAmount { get; set; } // Total amount for the entire sale (stored as integer in cents)

        public int? InvoiceID { get; set; } // Nullable Foreign Key to Invoices table

        public string PaymentMethod { get; set; } // Payment method (e.g., Credit Card, PayPal)

        public int ProcessedBy { get; set; } // Foreign Key to Users (Admin who processed the sale)

        public string Status { get; set; } // Sale status (Pending, Completed)

        public string ShippingAddress { get; set; } // Shipping address for the sale
    }
    public class ApproveSale
    {
        public string Status { get; set; }
    }
}
