using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_entrega_2.Models
{
    public class ProyectoBuscarModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public double Monto { get; set; }
        public int CuotasAPagar { get; set; }
        public double MontoCuota { get; set; }
        public double TasaInteresSegunCuota { get; set; }
        public string Imagen { get; set; }
        public DateTime FechaPresentacion { get; set; }
        public string Estado { get; set; }
        public Solicitante Solicitante { get; set; }
    }
}