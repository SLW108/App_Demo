using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Datos
{
    public class RepoInversor : IRepositorio<Inversor>
    {
        public bool Add(Inversor inversor)
        {

            bool ret = false;
            try
            {
                using (
                    var db = new P2PContext())
                {
                    if (FindById(inversor.Id) == null && FindByCi(inversor.Ci) == null)
                    {
                        db.Inversores.Add(inversor);
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

        public IEnumerable<Inversor> FindAll()
        {
            throw new NotImplementedException();
        }

        public Inversor FindById(int id)
        {
            try
            {
                using (var db = new P2PContext())
                {
                    var inversor = db.Inversores.Find(id);
                    return inversor;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Inversor FindByCi(string ci)
        {
            try
            {
                using (var db = new P2PContext())
                {
                    var inversor = db.Inversores.Where(u => u.Ci == ci).SingleOrDefault();
                    return inversor;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Inversor FindBy_T(object clave)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Inversor unT)
        {
            throw new NotImplementedException();
        }

        public bool Update(Inversor unT)
        {
            throw new NotImplementedException();
        }
    }
}
