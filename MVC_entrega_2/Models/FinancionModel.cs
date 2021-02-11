using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_entrega_2.Models
{
    public class FinancionModel
    {     

        public int Id { get; set; }
        public int ProyectoId { get; set; }

        public ProyectoModel proyectoModel { get; set; }
       
        public string CiInversor { get; set; }

        public double MontoAFinanciar { get; set; }

        public DateTime FechaInversion { get; set; }
        

    }
}