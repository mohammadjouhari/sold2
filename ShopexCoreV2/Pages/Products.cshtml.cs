using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ShopexCoreV2.Data;
using ShopexCoreV2.Models;
using System.Data;

namespace ShopexCoreV2.Pages
{
    public class ProductsModel : PageModel
    {
        public string connectionString { get; set; }
        public ProductsModel(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("sold");
        }
        // Works only for post request,not get;
        //  [BindProperty]
       // [BindProperty(SupportsGet = true)]
        [BindProperty]
        public List<Product> Products { get; set; }



        public IActionResult OnGet()
        {
            //Cehck is users is logged in;
            var user = HttpContext.Session.Get("User");
            if(user == null)
            {
               return RedirectToPage("CreateAccount");
            }
            else
            {
                Products = new List<Product>();
                using (var reader = SqlHelper1.ExecuteReader(connectionString, CommandType.StoredProcedure, "GetAllProducts", null))
                {
                    Products = reader.Select(r =>
                    new Product
                    {
                        Id = (int)r["Id"],
                        Title = r["Title"].ToString(),
                        Type = r["Type"].ToString(),
                        Model = r["ModelNumber"].ToString(),
                        Price = int.Parse(r["Price"].ToString()),
                        ImagePath = "Images" + "\\" + "ProductImages" + "\\" + r["ImagePath"].ToString(),
                        Description = r["Description"].ToString(),
                        Email = r["Email"].ToString(),
                        Mobile = r["Mobile"].ToString(),
                    }).ToList();
                }
                //"https:" + "\\" + Request.Host.Value + "\\"
                return this.Page();
            }
            
        }
    }
}
