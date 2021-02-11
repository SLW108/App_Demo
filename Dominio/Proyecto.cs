using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio
{
    [Table("Proyecto")]
    public class Proyecto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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
        [ForeignKey("Solicitante")]
        public int SolicitanteId { get; set; }
        public Solicitante Solicitante { get; set; }
        public string Tipo { get; set; }

        //Realizo calculos para persistir datos pertinentes al momento
        //de dar de alta los proyectos en la nueva base los cuales van a ser
        //de uso util al momento de la fianciacion
        public void RealizamosCalculos()
        {
            AsignarMontoCuotaSinInt();
            AsignarMontoCuotaIntInc();
            AsignarMontoInteresInc();
        } 
        public void AsignarMontoCuotaSinInt()
        {
            MontoCuotaSinInt = Monto / CuotasAPagar;
        }
        public void AsignarMontoCuotaIntInc()
        {
            double montoConInteres = ((Monto / 100) * TasaInteresSegunCuota) + Monto; 
            MontoCuotaIntInc = montoConInteres / CuotasAPagar;
        } 
        public void AsignarMontoInteresInc()
        {
            MontoTotalConIntereses = Monto + (Monto * TasaInteresSegunCuota);
        }

        //Actualizar Saldo
        public void ActualizoAlFinanciar(double montoAFinanciar) {
            this.SaldoRestanteFinanciar = SaldoRestanteFinanciar - montoAFinanciar;
            if (SaldoRestanteFinanciar==0) Estado = "CERRADO";           
        }

        public virtual bool ValidoReglasNegocioProyecto()
        {
            return true;
        }

        public override string ToString()
        {
            string delimitador = " | ";
            return Id.ToString() + delimitador + Titulo.ToString() + delimitador + Descripcion.ToString() + delimitador + Monto.ToString() + delimitador
                   + CuotasAPagar.ToString() + delimitador + TasaInteresSegunCuota.ToString() + delimitador + Imagen.ToString() + delimitador
                   + FechaPresentacion.ToString("dd-MM-yyyy") + delimitador + Estado.ToString() + delimitador + Solicitante.Id.ToString();
        }
       


















    }
}
