using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio
{
    public class Inversor : Usuario
    {
        public double MontoEstipulado { get; set; }
        public string Presentacion { get; set; }


        //Completar metodos 
        public override bool IsValidUser()
        {
            return base.IsValidUser() && Presentacion.Length > 0 && MontoEstipulado > 0;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
