using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio
{
    [Table("Usuario")]
    public abstract class Usuario
    {
        //Evaluar que es pertinente agregar con Data Annotations o Fluent API ej: Ci "Unique"
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [StringLength(8)]
        [Index(IsUnique = true)]
        public string Ci { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Password { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }

        //REPASAR TODAS LAS VALIDACIONES NO OLVIDAR

        #region VALIDACIONES
        public virtual bool IsValidUser()
        {
            bool esValido = false;
            if (IsValidCi(this.Ci) && IsValidPassWord(this.Password) && IsValidCel() && IsValidEmail() && IsValidFechaNacimiento()) esValido = true;
            return esValido;
        }

        public bool IsValidCi(string ci)
        {
            bool valido = false;

            if (!String.IsNullOrEmpty(ci))
            {
                
                if (int.TryParse(ci, out int num) & ci.Length == 8)
                {
                    valido = true;
                }
            }
            return valido;
        }

        public bool IsValidPassWord(string password)
        {
            bool valido = false;
            if (!String.IsNullOrEmpty(password))
            {
                if (password.Length >= 6)
                {
                    for (int i = 0; i < password.Length & !valido; i++)
                    {
                        if (Char.IsUpper(password[i]))
                        {
                            for (int y = 0; y < password.Length & !valido; y++)
                            {
                                if (Char.IsLower(password[y]))
                                {
                                    for (int z = 0; z < password.Length & !valido; z++)
                                    {
                                        
                                        if (int.TryParse(password[z].ToString(), out int num))
                                        {
                                            valido = true;
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
            return valido;
        }
                 
        public bool IsValidCel()
        {
            bool valido = false;
            if (!String.IsNullOrEmpty(this.Celular))
            {
                int Numero;
                bool exito = Int32.TryParse(Celular, out Numero);
                valido = exito;
            }
            return valido;
        }

        public bool IsValidEmail()
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(this.Email);
                return addr.Address == this.Email;
            }
            catch
            {
                return false;
            }
        }

        public bool IsValidFechaNacimiento()
        {
            bool esValido = false;
            int anioNacimiento = this.FechaNacimiento.Year;
            int anioActual = DateTime.Now.Year;
            if (21 <= (anioActual - anioNacimiento)) esValido = true;
            ////A prepo
            //esValido = true;
            return esValido;
        }
        #endregion 

        public virtual void GeneratePassWord()
        {
            string passwordPrimerCaracter = Nombre.Substring(0, 1).ToLower();
            string passwordSegundoCaracter = Apellido.Substring(0, 1).ToUpper();
            Password = passwordPrimerCaracter + passwordSegundoCaracter + Ci;
        }
        public override string ToString()
        {
            string delimitador = " | ";
            return Id.ToString() + delimitador + Ci.ToString() + delimitador + Nombre.ToString() + delimitador + Apellido.ToString()
                + delimitador + FechaNacimiento.ToString("dd-MM-yyyy") + delimitador
                                    + Celular.ToString() + delimitador + Email.ToString() + delimitador + Rol.ToString();

        }


    }
}
