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


        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 10;



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


                if (!string.IsNullOrWhiteSpace(SearchTerm))
                {
                    var term = SearchTerm.Trim().ToLower();
                    Products = Products.Where(p =>
                        p.Title.ToLower().Contains(term) ||
                        p.Model.ToLower().Contains(term) ||
                        p.Type.ToLower().Contains(term)).ToList();
                }

                // 2. Total Count & Page Bounds
                int count = Products.Count();
                TotalPages = (int)Math.Ceiling(count / (double)PageSize);
                if (TotalPages < 1) TotalPages = 1;

                if (PageIndex < 1) PageIndex = 1;
                if (PageIndex > TotalPages) PageIndex = TotalPages;

                // 3. Paginated Data Fetch
                Products = Products
                    .OrderByDescending(p => p.Id)
                    .Skip((PageIndex - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();




                //"https:" + "\\" + Request.Host.Value + "\\"
                return this.Page();
            }
            
        }
    }
}
