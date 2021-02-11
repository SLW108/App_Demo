using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using System.Data.Entity;

namespace Datos
{
    public class RepoSolicitante : IRepositorio<Solicitante>
    {
        public bool Add(Solicitante s)
        {
            bool ret = false;
            try
            {
                using (
                    var db = new P2PContext())
                {
                    if (FindById(s.Id) == null && FindByCi(s.Ci) == null)
                    {
                        db.Usuarios.Add(s);
                        db.SaveChanges();
                        ret = true;
                    }
                    else
                    {
                        Console.WriteLine("Este usuario ya existe en la base");
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

        public IEnumerable<Solicitante> FindAll()
        {
            try
            {
                using (
                    var db = new P2PContext())
                {
                    var solicitantes = db.Solicitantes.ToList<Solicitante>();
                    return solicitantes;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return null;
            }

        }

        public Solicitante FindById(int id)
        {
            try
            {
                using (var db = new P2PContext())
                {
                    var solicitante = db.Solicitantes.Find(id);
                    return solicitante;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Solicitante FindByCi(string ci)
        {
            try
            {
                using (var db = new P2PContext())
                {
                    var solicitante = db.Solicitantes.Where(u => u.Ci == ci).SingleOrDefault();
                    return solicitante;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Solicitante FindBy_T(object clave)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Solicitante unT)
        {
            throw new NotImplementedException();
        }

        public bool Update(Solicitante s)
        {
            bool ret = false;
            try
            {
                using (
                    var db = new P2PContext())
                {
                    db.Entry(s).State = EntityState.Modified;
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
    }
}
