using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Proyecto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public double Monto { get; set; }
        public int CuotasAPagar { get; set; }
        public double TasaInteresSegunCuota
        {
            get; set;
            /*get { return CacularTasaInteresSegunCuota(); }
            set
            {
                TasaInteresSegunCuota = CacularTasaInteresSegunCuota();
            }*/
           
            //get { return _myProperty; }
            //set { _myProperty = value; }

        }//dudas  sobre si tene que haber metodo y propiedad
        public string Imagen { get; set; }
        public DateTime FechaPresentacion => DateTime.Now;// Ver si vale la  pena usar de esta manera
        public string Estado { get; set; }//'APROBADO','RECHAZADO','PENDIENTE'
        public Usuario Usuario { get; set; }
        public virtual double MontoTope => 100000;


        //Falta Agregar una validacion del resto de los datos,que no sean null o vacios etc.

        public virtual bool esValidoMonto()
        {
            return (this.Monto <= MontoTope) ? true : false;        
        }
        public bool esValidoTopeMaxMinCuotas()
        {
            return (this.CuotasAPagar >= 3 & this.CuotasAPagar <= 12) ? true : false;
        }
        public virtual bool ValidoReglasNegocioProyecto()
        {
            return (esValidoMonto() & esValidoTopeMaxMinCuotas()) ? true : false;
        }

        public void CacularTasaInteresSegunCuota()
        {
            double TasaInteres;
            switch (CuotasAPagar)
            {
                case 3:
                default:
                    TasaInteres = 10;
                    break;
                case 6:
                    TasaInteres = 10.5;
                    break;
                case 12:
                    TasaInteres = 11;
                    break;
            }
            this.TasaInteresSegunCuota =  TasaInteres;
        }

        //Monto x cuota con intereses incluidos
        //Al momento del registro del prestamo para el resumen del punto 3
        //Evaluar si al momento de listar los prestamos se usa esto, o se levanta esta info de la base (habria que guardarla primero)
        public double MontoCuota()
        {
            double montoXCuota;
            montoXCuota = ((TasaInteresSegunCuota * Monto) + Monto) / CuotasAPagar;
            return montoXCuota;
        }
        public double MontoFinalInteresInc()
        {
            return Monto + (Monto * TasaInteresSegunCuota);
        }




        

      

      
       


       


        


    }
}
