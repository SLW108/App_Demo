using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Datos
{
    public class RepoFinanciacion : IRepositorio<Financiacion>
    {
        RepoProyecto repoProyecto = new RepoProyecto();
        RepoInversor repoInversor = new RepoInversor();
        public bool Add(Financiacion f)
        {          
            bool ret = false;
            try
            {
                using (
                    var db = new P2PContext())
                {
                    if (FindById(f.Id) == null)
                    {
                        Proyecto p = repoProyecto.FindById(f.ProyectoId);
                        if (p != null && p.Estado.Equals("ABIERTO"))
                        {
                            if (repoInversor.FindByCi(f.CiInversor) != null)
                            {
                                p.ActualizoAlFinanciar(f.MontoAFinanciar);
                                if (repoProyecto.Update(p))
                                {
                                    ////por las dudas
                                    f.Proyecto = null; //a la base solo el Id, es FK y alcanza
                                    ///
                                    f.FechaInversion = DateTime.Now.Date;
                                    db.Financiaciones.Add(f);
                                    db.SaveChanges();
                                    ret = true;
                                }
                               
                            }else{
                                Console.WriteLine("No existe inversor registrado " +
                                                    "en la base con este id");
                            }                       
                        }
                        else
                        {
                            Console.WriteLine("El proyecto con este id no existe en la base, o ya fue financiado(CERRADO)");
                        }
                    }                 
                    else
                    {
                        Console.WriteLine("Este prestamo ya existe en la base");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message.ToString());
                return false;
            }
            return ret;
        }

        public IEnumerable<Financiacion> FindAll()
        {
            try
            {
                using (
                    var db = new P2PContext())
                {
                    var financiaciones = db.Financiaciones
                                            .Include("Proyecto")
                                            .Include(f => f.Proyecto.Solicitante)
                                            .ToList();
                    return financiaciones;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return null;
            }
        }
        public IEnumerable<Financiacion> FindAll_X_CiInversor_Ordenado(string ciInversor)
        {
            try
            {
                using (
                    var db = new P2PContext())
                {
                    var financiaciones = db.Financiaciones
                                           .Include("Proyecto")
                                           .Include(f=>f.Proyecto.Solicitante)
                                           .Where(f=>f.CiInversor == ciInversor)
                                           .OrderByDescending(f=>f.FechaInversion)
                                           .OrderByDescending(f => f.MontoAFinanciar).ToList();
                    
                    return financiaciones;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return null;
            }
        }

        public Financiacion FindById(int id)
        {
            try
            {
                using (var db = new P2PContext())
                {
                    var financiacion = db.Financiaciones
                                          .Include("Proyecto")
                                          .Where(f=>f.Id==id)
                                          .SingleOrDefault();
                    return financiacion;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Financiacion FindBy_T(object clave)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Financiacion unT)
        {
            throw new NotImplementedException();
        }

        public bool Update(Financiacion unT)
        {
            throw new NotImplementedException();
        }
    }
}
