using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Datos;
using Dominio;

namespace WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ServicioDeArchivos : IServicioDeArchivos
    {
        public bool cargarUsuarios(string archivo, string tipo)
        {
            if (Archivos.leerArchivos(archivo, tipo))
            {
                return true;
            }
            return false;
        }

        public List<ProyectoDTO> FindAllProyectos()
        {
            RepoProyecto repoProyecto = new RepoProyecto();
            IEnumerable<Proyecto> proyectos = repoProyecto.FindAll();
            if (proyectos == null) return null;
            List<ProyectoDTO> proyectosDTOs = new List<ProyectoDTO>();           
            foreach (Proyecto p in proyectos)
            {
                proyectosDTOs.Add(MapearproyectosDTO(p));
            }

            return proyectosDTOs;
        }

        public List<SolicitanteDTO> FindAllSolicitantes()
        {
            RepoSolicitante repoSolicitante= new RepoSolicitante();
            IEnumerable<Solicitante> solicitantes = repoSolicitante.FindAll();
            if (solicitantes == null) return null;
            List<SolicitanteDTO> solicitantesDTOs =  new List<SolicitanteDTO>();
            foreach (Solicitante s in solicitantes)
            {
                solicitantesDTOs.Add(MapearSolicitanteDTO(s));
            }

            return solicitantesDTOs;
        }

        ProyectoDTO MapearproyectosDTO(Proyecto p)
        {
            if (p == null) return null;
            ProyectoDTO pDto = new ProyectoDTO
            {
                Id = p.Id,
                Titulo = p.Titulo,
                Descripcion = p.Descripcion,
                Monto = p.Monto,
                SaldoRestanteFinanciar = p.SaldoRestanteFinanciar,
                CuotasAPagar = p.CuotasAPagar,
                TasaInteresSegunCuota = p.TasaInteresSegunCuota,
                MontoCuotaSinInt = p.MontoCuotaSinInt,
                MontoTotalConIntereses = p.MontoTotalConIntereses,
                MontoCuotaIntInc = p.MontoCuotaIntInc,
                Imagen = p.Imagen,
                FechaPresentacion = p.FechaPresentacion,
                Estado = p.Estado,
                Solicitante = MapearSolicitanteDTO(p.Solicitante),
                Tipo = p.Tipo
            };
            
            if (p is ProyectoCooperativo)
            {
                ProyectoCooperativo pC = p as ProyectoCooperativo;
                pDto.CantIntegrantes = pC.CantIntegrantes;
            }
            else if (p is ProyectoPersonal)
            {
                ProyectoPersonal pP = p as ProyectoPersonal;
                pDto.ExpertisSolicitante = pP.ExpertisSolicitante;
            }
           
            return pDto;
        }

        SolicitanteDTO MapearSolicitanteDTO(Solicitante s)
        {
            if (s == null) return null;
            SolicitanteDTO sDto = new SolicitanteDTO
            {
                Id = s.Id,
                Ci = s.Ci,
                Nombre = s.Nombre,
                Apellido = s.Apellido,
                FechaNacimiento = s.FechaNacimiento,
                Celular = s.Celular,
                Email = s.Email,
                Rol = s.Rol,
            };
            return sDto;
        }
    }
}
