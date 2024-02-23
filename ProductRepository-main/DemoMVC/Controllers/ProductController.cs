using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Build.Evaluation;
namespace DemoMVC.Controllers
{
    public class ProductController : Controller
    {
        public string connectionString = "Server=192.168.0.23,1427;Initial Catalog=interns;Integrated Security=False;user id=Interns;password=test;";
        [Authorize]
        
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
       // [ValidateAntiForgeryToken]
        [Route("read")]
        [Authorize]
        public IActionResult ReadProduct()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("read")]
        public IActionResult ReadProduct(Product obj)
        {
            return RedirectToAction("DispDetails", new { sku = obj.SKU });
            /*using (IDbConnection sql = new SqlConnection(connectionString))
            {
                obj = sql.QueryFirstOrDefault<Product>(selectCmd, new { input = sku });
                ViewBag.ProductId = "Product ID : "+obj.ProductId;
                ViewBag.SKU = "SKU : " + obj.SKU;
                ViewBag.ProductName = "Product Name : " + obj.ProductName;
                ViewBag.Features = "Features : " + obj.Features;
                
            }*/
        }

        /*[HttpGet]
        [Route("Display")]
        [Authorize]
        public IActionResult DispDetails() { return View(); }
        [HttpPost]*/
        [Route("Display")]
        [Authorize]
       // [ValidateAntiForgeryToken]
        public IActionResult DispDetails(string sku)
        {
            string selectCmd = "Select * from Products Where SKU = @input";
            Product? obj;
            using (IDbConnection sql = new SqlConnection(connectionString))
            {
                obj = sql.QueryFirstOrDefault<Product>(selectCmd, new { input = sku });
                return View(obj);
            }
        }
        /*[Route("read")]
        [Authorize]
        [OutputCache(Duration =60)]
        public async Task<IActionResult> ReadProduct(IFormCollection f)
        {
            string sku = f["SKU"];
            string selectCmd = "Select * from Products Where SKU = @input";
            string connectionString = "Server=192.168.0.23,1427;Initial Catalog=interns;Integrated Security=False;user id=Interns;password=test;";
            List<Product> productList = new List<Product>();
            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                productList = (await conn.QueryAsync<Product>(selectCmd, new { input = sku })).ToList();
            }
            if (productList.Count == 0)
            {
                Console.WriteLine("List is empty");
            }
            foreach (var val in productList)
            {
                ViewBag.ProductId = val.ProductId;
                ViewBag.SKU = val.SKU;
                ViewBag.ProductName = val.ProductName;
                ViewBag.Features = val.Features;
                ViewBag.Price = val.Price;
                ViewBag.Stock = val.Stock;
                ViewBag.Warranty = val.Warranty;
                ViewBag.Rating = val.Rating;
                ViewBag.VendorId = val.VendorId;
            }
            return View();
        }*/
        [OutputCache(Duration = 60)]
        [Route("view")]
        [Authorize]
       // [ValidateAntiForgeryToken]
        public IActionResult ViewProduct()
        {
            string selectCmd = "Select * from Products";

            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                var product = conn.Query<Product>(selectCmd).ToList();
                return View(product);
            }
        }
        [Route("insert")]
        [HttpGet]
       // [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Route("insert")]
       // [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            string? ProductId, SKU, ProductName, Features;
            decimal Price;
            int VendorId, Rating, Stock, Warranty;
            ProductId = product.ProductId;
            SKU = product.SKU;
            ProductName = product.ProductName;
            Features = product.Features;
            Price = Convert.ToDecimal(product.Price);
            VendorId = Convert.ToInt32(product.VendorId);
            Rating = Convert.ToInt32(product.Rating);
            Stock = Convert.ToInt32(product.Stock);
            Warranty = Convert.ToInt32(product.Warranty);
            string insertCmd = "Insert into Products (ProductId,SKU,ProductName,Features,Price,Stock,Warranty,Rating,VendorId) Values (@ProductId,@SKU,@ProductName,@Features,@Price,@Stock,@Warranty,@Rating,@VendorId)";
            var values = new { ProductId = ProductId, SKU = SKU, ProductName = ProductName, Features = Features, Price = Price, Stock = Stock, Warranty = Warranty, Rating = Rating, VendorId = VendorId };
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(insertCmd, values);
            }
            Console.WriteLine("Successful insertion");
            ViewData["Message"] = "Successful insertion of Product details";
            return View();
        }
        [Route("update")]
        [HttpGet]
      //  [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult UpdateStock()
        {
            return View();
        }
        [HttpPost]
        [Route("update")]
       // [ValidateAntiForgeryToken]
        public IActionResult UpdateStock(UpdateProduct f)
        {
            if (ModelState.IsValid)
            {
                int stock = Convert.ToInt32(f.Stock);
                string? SKU = f.SKU;
                string updateCmd = "Update Products Set Stock =@Stock Where SKU = @SKU";
                var val = new { Stock = stock, SKU = SKU };
                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    connection.Execute(updateCmd, val);
                }
                Console.WriteLine("Updation completed");
                ViewData["Message"] = "Stock details updated successfully";
                return View();
            }
            else
            {
                return View();
            }
            
        }
        [Route("delete")]
        [HttpGet]
        [Authorize]
       // [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct() { return View(); }
        [HttpPost]
        [Route("delete")]
       // [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(DeleteProduct product)
        {
            string? SKU = product.SKU;
            var val = new {  SKU = SKU };
            string deleteCmd = "Delete from Products Where SKU = @SKU";
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(deleteCmd, val);
            }
            Console.WriteLine("Deletion completed");
            ViewData["Message"] = "Product details deleted successfully";
            return View();
        }
    }
}