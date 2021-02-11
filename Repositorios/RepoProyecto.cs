using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;

namespace Datos
{
    public class RepoProyecto : IRepositorio<Proyecto>
    {
        public bool Add(Proyecto p)
        {

            bool ret = false;
            try
            {
                using (
                    var db = new P2PContext())
                {
                    if (FindById(p.Id) == null)
                    {
                        db.Proyectos.Add(p);
                        db.SaveChanges();
                        ret = true;
                    }
                    else
                    {
                        Console.WriteLine("Este proyecto ya existe en la base");
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

        public IEnumerable<Proyecto> FindAll()
        {
            try
            {
                using (
                    var db = new P2PContext())
                {
                    var proyectos = db.Proyectos.AsQueryable();
                    proyectos = proyectos.Include("Solicitante");
                    return proyectos.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return null;
            }
        }

        public Proyecto FindById(int id)
        {
            try
            {
                using (var db = new P2PContext())
                {                   
                    var proyecto = db.Proyectos.Include("Solicitante").Where(x=>x.Id == id).SingleOrDefault();                                  
                    return proyecto;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Proyecto FindBy_T(object clave)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Proyecto unT)
        {
            throw new NotImplementedException();
        }

        public bool Update(Proyecto p)
        {
            bool ret = false;
            try
            {
                using (
                    var db = new P2PContext())
                {
                    db.Entry(p).State = EntityState.Modified;
                    if (db.SaveChanges() > 0) ret = true;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message.ToString());
                return false;
            }
            return ret;
        }

        public IEnumerable<Proyecto> BuscarYFiltrarProyectos(DateTime? fechaDesde = null, DateTime? fechaHasta = null,
                 string ci = null, string txtTitulo = null, string txtDescripcion = null, string estado = null, double? montoDado = null)
        {
            using (P2PContext db = new P2PContext())
            {
                var proyectosTodos = db.Proyectos.AsQueryable();
                proyectosTodos = proyectosTodos.Include("Solicitante");

                //Filtro entre dos fechas dadas
                if (fechaDesde != null) 
                {
                    proyectosTodos = proyectosTodos.Where(p => p.FechaPresentacion >= fechaDesde);
                }
                if ( fechaHasta != null)
                {
                    proyectosTodos = proyectosTodos.Where(p => p.FechaPresentacion <= fechaHasta);
                }
                //Filtro por Cedula
                if (!String.IsNullOrEmpty(ci))
                {
                    proyectosTodos = proyectosTodos.Include("Solicitante")
                                           .Where(p => p.Solicitante.Ci == ci);                    
                }
                //Filtro por texto en el titulo
                if (!String.IsNullOrEmpty(txtTitulo))
                {
                    proyectosTodos = proyectosTodos.Where(p => p.Titulo.Contains(txtTitulo));
                }
                //Filtro por texto en el descripcion
                if (!String.IsNullOrEmpty(txtDescripcion))
                {
                    proyectosTodos = proyectosTodos.Where(p => p.Descripcion.Contains(txtDescripcion));
                }
                //Filtro por estado
                if (!String.IsNullOrEmpty(estado))
                {
                    proyectosTodos = proyectosTodos.Where(p => p.Estado.Equals(estado));
                }
                //Filtro por un monto dado
                if (montoDado != null && montoDado != 0)
                {
                    proyectosTodos = proyectosTodos.Where(p => p.Monto <= montoDado);
                }

                return proyectosTodos.ToList();
            }
        }



    }
}
