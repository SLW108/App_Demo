using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Datos;
using System.Data.Entity;
//using WCF;

namespace Consola
{
    class Program
    {
        
        static void Main(string[] args)
        {
            RepoFinanciacion repoFinanciacion = new RepoFinanciacion();
            IEnumerable<Financiacion> finac = repoFinanciacion.FindAll();
            IEnumerable<Financiacion> financiaciones = repoFinanciacion.FindAll_X_CiInversor_Ordenado("12345678");

            //foreach (Financiacion financiacion in finac)
            //{
            //    Console.WriteLine(financiacion.MontoAFinanciar.ToString() + financiacion.Proyecto.Descripcion + financiacion.CiInversor);
            //}

            using (P2PContext db = new P2PContext())
            {
                db.Database.CreateIfNotExists();
                /*Console.WriteLine(succes);*/
                //Console.ReadLine();
            }

            #region FiltroProyectos
            //IEnumerable<Proyecto> BuscarYFiltrarProyectos(DateTime? fechaDesde = null, DateTime? fechaHasta = null, 
            //     string ci = null, string txtTitulo = null, string txtDescripcion = null, string estado = null, double? montoDado=null)
            //{
            //    using (P2PContext db = new P2PContext())
            //    {
            //        var proyectosTodos = db.Proyectos.AsQueryable();
            //        //Filtro entre dos fechas dadas
            //        if (fechaDesde != null && fechaHasta!=null) { 
            //         proyectosTodos = proyectosTodos.
            //                             Where(p => p.FechaPresentacion >= fechaDesde 
            //                                   && p.FechaPresentacion <= fechaHasta);
            //        }
            //        //Filtro por Cedula
            //        if (!String.IsNullOrEmpty(ci))
            //        {
            //            proyectosTodos = proyectosTodos.Include("Solicitante")
            //                                   .Where(p => p.Solicitante.Ci == ci);
            //        }
            //        //Filtro por texto en el titulo
            //        if (!String.IsNullOrEmpty(txtTitulo))
            //        {
            //            proyectosTodos = proyectosTodos.Where(p => p.Titulo.Contains(txtTitulo));
            //        }
            //        //Filtro por texto en el descripcion
            //        if (!String.IsNullOrEmpty(txtDescripcion))
            //        {
            //            proyectosTodos = proyectosTodos.Where(p => p.Descripcion.Contains(txtDescripcion));
            //        }
            //        //Filtro por estado
            //        if (!String.IsNullOrEmpty(estado))
            //        {
            //            proyectosTodos = proyectosTodos.Where(p => p.Estado.Equals(estado));
            //        }
            //        //Filtro por un monto dado
            //        if (montoDado!= null)
            //        {
            //            proyectosTodos = proyectosTodos.Where(p => p.Monto <= montoDado);
            //        }

            //        return proyectosTodos.ToList();
            //    }
            //}





            //DateTime fechaDesdeA =Convert.ToDateTime("11-10-2020").Date;
            //DateTime fechaHastaB = Convert.ToDateTime("12-10-2020").Date;
            //string cedula = "37050664";
            //string titulo = "NTVG";
            //string descripcion = "madre";
            //string estadoX = "ABIERTO";



            //IEnumerable<Proyecto> proyectosXFecha = BuscarYFiltrarProyectos(fechaDesdeA, fechaHastaB, cedula, 
            //                                                                titulo,descripcion, estadoX, 10000);

            //IEnumerable<Proyecto> proyectosXFechaA = BuscarYFiltrarProyectos(fechaDesdeA, fechaHastaB, null,
            //                                                                null, null, null, null);

            //IEnumerable<Proyecto> proyectosXFechaB = BuscarYFiltrarProyectos(fechaDesdeA, fechaHastaB, cedula,
            //                                                                null, null, null, null);

            //IEnumerable<Proyecto> proyectosXCedulaC = BuscarYFiltrarProyectos(null, null, cedula,
            //                                                                titulo, null,null, null);

            //IEnumerable<Proyecto> proyectosXCedulax = BuscarYFiltrarProyectos(null, null, null,
            //                                                                null, "ppp", null, null);

            //IEnumerable<Proyecto> proyectosXCedulaD = BuscarYFiltrarProyectos(null, null, cedula,
            //                                                                titulo, null, "CERRADO", null);

            #endregion




            /*
            RepoProyecto repoProyectos = new RepoProyecto();
            IEnumerable<Proyecto> lista = repoProyectos.FindAll();

            foreach (Proyecto p in lista)
            {
                Console.WriteLine(p.Titulo);
            }
            *

            /*  RepoSolicitante repoSolicitante = new RepoSolicitante();
             Solicitante usuario = new Solicitante()
             {
                 Id = 2,
                 Nombre = "German",
                 Apellido = "Ocampo",
                 Ci = "61767186",
                 Celular = "099977911",
                 Email = "pepe@gmail.com",
                 Rol = "Solicitante",
                 FechaNacimiento = DateTime.Now
             };
             usuario.GeneratePassWord();

             bool exito = repoSolicitante.Add(usuario);
             */

            Archivos.leerArchivos("usuarios.txt", "usuario");
            Archivos.leerArchivos("proyectos.txt", "proyecto");

            #region Alta Financiacion
            //RepoProyecto repoProyecto = new RepoProyecto();
            RepoInversor repoInversor = new RepoInversor();
            ////RepoFinanciacion repoFinanciacion = new RepoFinanciacion();

            Inversor i = new Inversor()
            {
                Id = 15,
                Nombre = "Inversor",
                Apellido = "Nuevo",
                Ci = "44014235",
                Celular = "099977911",
                Email = "Inversore@gmail.com",
                Rol = "Inversor",
                FechaNacimiento = DateTime.Now,
                Password = "Sl099330859",
                MontoEstipulado = 100000,
                Presentacion = "Tengo mucho dinero"

            };
            repoInversor.Add(i);

            
            Inversor iBuscado = repoInversor.FindByCi("44014235");//el que esta logueado 

            Financiacion f = new Financiacion()
            {
                ProyectoId = 18,// hay que mandar soloe el ID
                CiInversor = iBuscado.Ci,
                MontoAFinanciar = 20 //// es el que se ingresa por interfaz
            };
            Financiacion f2 = new Financiacion()
            {
                ProyectoId = 18,// hay que mandar soloe el ID
                CiInversor = iBuscado.Ci,
                MontoAFinanciar = 30 //// es el que se ingresa por interfaz
            };
            Financiacion f3 = new Financiacion()
            {
                ProyectoId = 18,// hay que mandar soloe el ID
                CiInversor = iBuscado.Ci,
                MontoAFinanciar = 40 //// es el que se ingresa por interfaz
            };
            Financiacion f4 = new Financiacion()
            {
                ProyectoId = 18,// hay que mandar soloe el ID
                CiInversor = iBuscado.Ci,
                MontoAFinanciar = 20 //// es el que se ingresa por interfaz
            };
            Financiacion f5 = new Financiacion()
            {
                ProyectoId = 18,// hay que mandar soloe el ID
                CiInversor = iBuscado.Ci,
                MontoAFinanciar = 10 //// es el que se ingresa por interfaz
            };
            Financiacion f6 = new Financiacion()
            {
                ProyectoId = 18,// hay que mandar soloe el ID
                CiInversor = iBuscado.Ci,
                MontoAFinanciar = 15 //// es el que se ingresa por interfaz
            };
            Financiacion f7 = new Financiacion()
            {
                ProyectoId = 19,
                CiInversor = iBuscado.Ci,
                MontoAFinanciar = 50 //// es el que se ingresa por interfaz
            };

             repoFinanciacion.Add(f);
            repoFinanciacion.Add(f2);
            repoFinanciacion.Add(f3);
            repoFinanciacion.Add(f4);
            repoFinanciacion.Add(f5);
            repoFinanciacion.Add(f6);
            repoFinanciacion.Add(f7);
            
            ////if (exitoAltaFina)
            //    {
            //        Console.WriteLine("Este monto deberia ser 5000: "+ repoProyecto.FindById(17).SaldoRestanteFinanciar.ToString());
            //    }
            IEnumerable<Financiacion> financiacionsFiltradasYOrden = repoFinanciacion.FindAll_X_CiInversor_Ordenado(i.Ci);

            foreach (Financiacion financiacion in financiacionsFiltradasYOrden)
            {
                Console.WriteLine(financiacion.MontoAFinanciar.ToString() + financiacion.Proyecto.Descripcion + financiacion.CiInversor);
            }
            #endregion


            #region
            /*try
            {
                ServicioDeArchivos proxy = new ServicioDeArchivos();
                proxy.Open();
                Console.WriteLine("Por favor ingrese el 1er número de la suma");
                string operador1 = Console.ReadLine();
                Console.WriteLine("Por favor ingrese el 2do número de la suma");
                string operador2 = Console.ReadLine();
                Calculadora c = proxy.SumarConCalc(new Calculadora { a = int.Parse(operador1), b = int.Parse(operador2) });
                proxy.Close();
                Console.WriteLine("El resultado es: " + c.Resultado);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                throw;
            }*/
            #endregion

            Console.ReadLine();
            
        }
    }
}
