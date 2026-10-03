using System.IO;
namespace ShopexCoreV2.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public string Mobile { get; set; }
        public string Passwprd { get; set; }
        public string ConfirmPasswprd { get; set; }
        public string Email { get; set; }
    }
}
