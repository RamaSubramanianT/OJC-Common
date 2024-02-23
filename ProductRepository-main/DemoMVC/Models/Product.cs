using System.ComponentModel.DataAnnotations;
namespace DemoMVC.Models
{
    public class Product
    {
        public string? ProductId { get; set; }
        public string? SKU { get; set; }
        public string? ProductName { get; set; }
        public string? Features { get; set; }

        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock value can't be empty")]
        public int Stock { get; set; }

        public int Warranty { get; set; }
        [Required(ErrorMessage = "Warranty can't be empty")]
        public required int Rating { get; set; }
        [Required(ErrorMessage = "Vendor ID can't be empty")]
        public int VendorId { get; set; }

    }
}
