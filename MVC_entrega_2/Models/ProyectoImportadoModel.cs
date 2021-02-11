using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_entrega_2.Models
{
    public class ProyectoImportadoModel
    {
        public string Titulo { get; set; }
        public string Imagen { get; set; }
        public string Estado { get; set; }
        public Solicitante Solicitante { get; set; }
    }
}