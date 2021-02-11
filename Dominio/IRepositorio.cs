using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dominio
{
    public interface IRepositorio<T> where T : class
    {
        bool Add(T unT);
        bool Remove(T unT);
        bool Update(T unT);
        T FindBy_T(object clave);
        T FindById(int id);
        IEnumerable<T> FindAll();

      
    }
}
