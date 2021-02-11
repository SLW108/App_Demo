using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio
{
    [Table("Financiacion")]
    public class Financiacion
    {
        
        public int Id { get; set; }

        [ForeignKey("Proyecto")]
        public int ProyectoId { get; set; }
        public Proyecto Proyecto { get; set; } //  Solo para mostrar al momento del alta solo el IdProyecto

        public string CiInversor { get; set; }

        public double MontoAFinanciar { get; set; }
        public DateTime FechaInversion { get; set; }

      
        /*(menor o igual que el saldo restante a financiar sin intereses,
         * y menor o igual que el monto que dispuso cuando se registró).*/
        public bool MontoAFinanciarValido()
        {
            double montoSinInt = Proyecto.Monto;
            double montoRestante = Proyecto.SaldoRestanteFinanciar;
            double montoDispuestoInversor = 5;//Inversor.MontoEstipulado;
            if (MontoAFinanciar <= montoSinInt)
            {
                if (MontoAFinanciar <= montoRestante)
                {
                    if (MontoAFinanciar <= montoDispuestoInversor)
                    {
                        Console.WriteLine("Financiacion valida para dar de alta");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("La Financiacion no es valida, ya que el monto ingresado" +
                                            "es mayor al monto dispuesto por el inversor");
                    }
                }
                else
                {
                    Console.WriteLine("La Financiacion no es valida, ya que el monto ingresado" +
                        "es mayor al mosnto restante a financiar");
                }
            }
            else
            {
                Console.WriteLine("La Financiacion no es valida, ya que el monto ingresado " +
                    "es mayor a la del proyecto");
            }   
            return false;
        }



    }
}
