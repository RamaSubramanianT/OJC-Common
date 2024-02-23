using System.ComponentModel.DataAnnotations;
namespace DemoMVC.Models
{
    public class UpdateProduct:IProductRepository
    {
        [Required(ErrorMessage = "Enter a valid SKU")]
        public string? SKU { get; set; }
        [Required(ErrorMessage = "Stock value can't be empty")]
        public int Stock { get; set; }

        private List<UpdateProduct> products;
        
        public List<UpdateProduct> GetProduct()
        {
            products = new List<UpdateProduct>()
            {
                new UpdateProduct() {SKU="567",Stock=500},
                new UpdateProduct(){SKU="2867",Stock=965},
            };
            return products;
        }
    }
}
