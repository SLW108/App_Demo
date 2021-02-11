using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using System.Data.Entity;

//using System.Data;
//using System.Data.SqlClient;


namespace Datos
{
    public class RepoUsuario : IRepositorio<Usuario>
    {
        public bool Add(Usuario u)
        {
            bool ret = false;
            try
            {
                using (
                    var db = new P2PContext())
                    {  
                        if (FindById(u.Id)==null && FindByCi(u.Ci)==null)
                        {
                            db.Usuarios.Add(u);
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

        public IEnumerable<Usuario> FindAll()
        {          
            try
            {
                using (
                    var db = new P2PContext())
                {
                    var usuarios = db.Usuarios.ToList<Usuario>();
                    return usuarios;
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return null;
            }

        }

        public Usuario FindById(int id)
        {
            try
            {
                using (var db = new P2PContext())
                {
                    var usuario = db.Usuarios.Find(id);
                    return usuario;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Usuario FindByCi(string ci)
        {
            try
            {
                using (var db = new P2PContext())
                {
                    var usuario = db.Usuarios.Where(u=>u.Ci == ci).SingleOrDefault();
                    return usuario;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Usuario FindBy_T(object clave)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Usuario unT)
        {
            throw new NotImplementedException();
        }

        public bool Update(Usuario u)
        {
            bool ret = false;
            try
            {
                using (
                    var db = new P2PContext())
                {     
                    db.Entry(u).State = EntityState.Modified;
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
