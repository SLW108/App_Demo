using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominio;

namespace WebAPI.Models
{
    public class SolicitanteModel
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Ci { get; set; }
        public string Rol { get; set; }
    }
}