using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio
{
    public class Solicitante : Usuario
    {
        public override void GeneratePassWord()
        {
            base.GeneratePassWord();
        }
        public override string ToString()
        {
           return base.ToString();
        }
    }
}
