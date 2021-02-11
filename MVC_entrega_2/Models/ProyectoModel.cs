using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_entrega_2.Models
{
    public class ProyectoModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public double Monto { get; set; }
        public double SaldoRestanteFinanciar { get; set; }
        public int CuotasAPagar { get; set; }
        public double TasaInteresSegunCuota { get; set; }
        public double MontoCuotaSinInt { get; set; }
        public double MontoTotalConIntereses { get; set; }
        public double MontoCuotaIntInc { get; set; }
        public string Imagen { get; set; }
        public DateTime FechaPresentacion { get; set; }
        public string Estado { get; set; }       
        public int SolicitanteId { get; set; }
        public virtual Solicitante Solicitante { get; set; }
        public string Tipo { get; set; }
        public string ExpertisSolicitante { get; set; }
        public int CantIntegrantes { get; set; }
    }
}