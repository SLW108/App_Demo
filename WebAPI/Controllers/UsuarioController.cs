using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Datos;
using Dominio;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [RoutePrefix("api")]
    public class UsuarioController : ApiController
    {
        private P2PContext db = new P2PContext();
        RepoUsuario repoUsuarios = new RepoUsuario();


        [Route("usuarios")]
        // GET: api/usuarios
        public IHttpActionResult Get()
        {
            List<SolicitanteModel> listaUsuariosDTO = new List<SolicitanteModel>();
            try
            {
                List<Usuario> listaUsuarios = repoUsuarios.FindAll().ToList();
                if (listaUsuarios != null)
                {
                    foreach (Usuario s in listaUsuarios)
                    {
                        SolicitanteModel solicitante = new SolicitanteModel()
                        {
                            Nombre = s.Nombre,
                            Apellido = s.Apellido,
                            Ci = s.Ci,
                            Rol = s.Rol,
                        };

                        listaUsuariosDTO.Add(solicitante);
                    }

                }
            }
            catch (Exception e)
            {
                return InternalServerError();
            }

            return Ok(listaUsuariosDTO);

        }
    }
}