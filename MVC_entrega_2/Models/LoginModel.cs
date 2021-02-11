using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC_entrega_2.Models
{
    public class LoginModel
    {
        [Display(Name = "Cedula de Identidad"), Required]
        public int Ci { get; set; }

        [Display(Name = "Clave"), Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}