using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominio;

namespace WebAPI.Models
{
    public class ProyectoFormBuscarModel
    {
        public string txtTitulo { get; set; }
        public string txtDescripcion { get; set; }
        public double Monto { get; set; }

        public DateTime? fechaDesde { get; set; }
        
        public DateTime? fechaHasta { get; set; }
        public string estado { get; set; }

        public double montoDado { get; set; }
        
        public string ci { get; set; }
    }
}