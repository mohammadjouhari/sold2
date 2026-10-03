using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ShopexCoreV2.Data;
using ShopexCoreV2.Models;
using System.Data;
using System.Data.SqlClient;

namespace ShopexCoreV2.Pages
{

    public class CreateAccountModel : PageModel
    {
        public string connectionString { get; set; }
        public CreateAccountModel(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("sold");
        }

        protected User RegisteredUser { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            RegisteredUser = new User();
            RegisteredUser.Name = Request.Form["name"].ToString();
            RegisteredUser.Email = Request.Form["Email"].ToString();
            RegisteredUser.UserName = Request.Form["UserName"].ToString();
            RegisteredUser.Mobile = Request.Form["Mobile"].ToString();
            RegisteredUser.Country = Request.Form["Country"].ToString();
            RegisteredUser.Passwprd = Request.Form["Password"].ToString();
            RegisteredUser.ConfirmPasswprd = Request.Form["ConfirmPassword"].ToString();
            if (RegisteredUser.Country == "UAE")
            {
                RegisteredUser.Currency = "AED";
            }
            else if (RegisteredUser.Country == "KSA")
            {
                RegisteredUser.Currency = "SAR";
            }
            else if (RegisteredUser.Country == "Bahrain")
            {
                RegisteredUser.Currency = "BHD";
            }
            else if (RegisteredUser.Country == "Qatar")
            {
                RegisteredUser.Currency = "QAR";
            }
            else if (RegisteredUser.Country == "Oman")
            {
                RegisteredUser.Currency = "OMR";
            }
            else if (RegisteredUser.Country == "kuwait")
            {
                RegisteredUser.Currency = "KWD";
            }

            var parameters = new[]
            {
               new SqlParameter("@Name", SqlDbType.NVarChar) {Value = RegisteredUser.Name},
               new SqlParameter("@Email", SqlDbType.NVarChar) {Value = RegisteredUser.Email},
               new SqlParameter("@UserName", SqlDbType.NVarChar) {Value = RegisteredUser.UserName},
               new SqlParameter("@Mobile", SqlDbType.NVarChar) {Value = RegisteredUser.Mobile},
               new SqlParameter("@Country", SqlDbType.NVarChar) {Value = RegisteredUser.Country},
               new SqlParameter("@Password", SqlDbType.NVarChar) {Value = RegisteredUser.Passwprd},
               new SqlParameter("@Currency", SqlDbType.NVarChar) {Value = RegisteredUser.Currency},

            };
            SqlHelper1.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "InsertUser", parameters);
            HttpContext.Session.SetString("User", JsonConvert.SerializeObject(RegisteredUser));
            return RedirectToPage("Products");
        }
    }
}
