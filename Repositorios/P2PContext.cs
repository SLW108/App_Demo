using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Dominio;

namespace Datos
{
    public class P2PContext :DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Solicitante> Solicitantes { get; set; }

        public DbSet<Inversor> Inversores { get; set; }

        public DbSet<Proyecto> Proyectos { get; set; }

        public DbSet<Financiacion> Financiaciones { get; set; }

        public P2PContext() : base("con")
        {

        }
    }
}
