using System.ComponentModel.DataAnnotations;

namespace drinks.web.Models
{
    public class LoginViewModel
    {
        public class SecretKey
        {
            [Required]
            [Display(Name = "Секретный ключ")]
            public string Key { get; set; }
        }

    }
}