using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC_entrega_2.Models
{
    public class RegisterModel
    {
        [Display(Name = "Cedula de Identidad"), Required]
        public int Ci { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Display(Name = "Fecha de Nacimiento"), Required]
        public DateTime FechaNacimiento { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        
        [Display(Name = "Monto Total a invertir"), Required]
        public double MontoEstipulado { get; set; }
        
        [Display(Name = "Breve presentación de su persona"), Required]
        public string Presentacion { get; set; }

        [Display(Name = "Clave"), Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Repite la Clave"), Required]
        [DataType(DataType.Password)]
        public string PasswordConfirmar { get; set; }


        //[Required(ErrorMessage = "El Email es un campo requerido")]
        //[EmailAddress]
        //public string Email { get; set; }

        //[DataType(DataType.Password)]
        //[MinLength(6)]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[.#$^+=!*()@%&]).{6,20}$")] //Al menos un caracter, una mayuscula y un digito


    }
}