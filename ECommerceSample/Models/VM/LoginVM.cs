using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ECommerceSample.Models.VM
{
    public class LoginVM
    {
        [
EmailAddress(ErrorMessage = "E-Posta formatında giris yapınız"),
Required(ErrorMessage = "E-Posta giriniz"),
DisplayName("E-Posta")
]
        public string Email { get; set; }

        [
         Required(ErrorMessage = "Parola giriniz"),
         DisplayName("Parola")
        ]
        public string Password { get; set; }

        [DisplayName("Beni Hatırla")]
        public bool IsPersistant { get; set; }
    }
}