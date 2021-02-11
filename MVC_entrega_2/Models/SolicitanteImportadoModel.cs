using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_entrega_2.Models
{
    public class SolicitanteImportadoModel
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Ci { get; set; }
        public string Rol { get; set; }
    }
}