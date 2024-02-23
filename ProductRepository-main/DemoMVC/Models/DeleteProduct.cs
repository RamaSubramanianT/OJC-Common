using System.ComponentModel.DataAnnotations;
namespace DemoMVC.Models
{
    public class DeleteProduct
    {
        [Required(ErrorMessage = "SKU value is empty")]
        public string? SKU { get; set; }
    }
}
