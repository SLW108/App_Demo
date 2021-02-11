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
    public class ProyectosController : ApiController
    {
        private P2PContext db = new P2PContext();
        RepoProyecto repoProyectos = new RepoProyecto();
        RepoUsuario repoUsuarios = new RepoUsuario();
        RepoFinanciacion repoFinanciacion = new RepoFinanciacion();

        [Route("proyectos")]
        // GET: api/proyectos
        public IHttpActionResult Get()
        {
            List<ProyectoModel> listaProyectosDTO = new List<ProyectoModel>();
            try
            {
                List<Proyecto> listaProyectos = repoProyectos.FindAll().ToList();              
                if (listaProyectos != null)
                {
                    foreach (Proyecto p in listaProyectos)
                    {
                        ProyectoModel pM = new ProyectoModel()
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
                            Imagen =p.Imagen,
                            FechaPresentacion = p.FechaPresentacion,
                            Estado = p.Estado,
                            Solicitante = p.Solicitante,
                            Tipo = p.Tipo
                        };
                        if (p.Tipo.Equals("P"))
                        {
                            ProyectoPersonal pP = p as ProyectoPersonal;
                            pM.ExpertisSolicitante = pP.ExpertisSolicitante;
                        }
                        else
                        {
                            ProyectoCooperativo pP = p as ProyectoCooperativo;
                            pM.CantIntegrantes = pP.CantIntegrantes;
                        }
                        /*OJOOOOOOO, TENER en cuenta que hay que generar esta vista otra vez y compilar
                         ya que el PROYECTOMODEL FUE MODIFICADO, Y AL RENDERIZAR LA VISTA HAY QUE PONER UN IF
                         PREGUNTANDO QUE TIPO ES, PARA QUE NO SALTE UNA EXCEPTION POR NULL
                         SI ES COOPERATIVO, EXPERTIS ES NULL, SI ES PERSONAL CANTINTEGRANTES ES NULL*/
                        listaProyectosDTO.Add(pM);
                    }
                    
                }
            }
            catch (Exception e)
            {
                return InternalServerError();
            }

            return Ok(listaProyectosDTO);

        }

        [Route("proyectos/{id:int}", Name = "GetById")]
        // GET: api/proyectos/5
        [ResponseType(typeof(Proyecto))]
        public IHttpActionResult Get(int id)
        {
            IEnumerable<Proyecto> proyecto = db.Proyectos.Where(p => p.Id == id).ToList();
            Proyecto proyectoUno = proyecto.First();
            proyectoUno.Solicitante = (Solicitante)repoUsuarios.FindById(proyectoUno.SolicitanteId);
            if (proyectoUno == null)
            {
                return NotFound();
            }

            return Ok(proyectoUno);
        }

        [HttpGet]
        [Route("proyectos/buscar")]
        [ResponseType(typeof(List<ProyectoModel>))]
        public IHttpActionResult Buscar([FromUri] Models.ProyectoFormBuscarModel datos)
        {
            List<ProyectoModel> proyectosFiltradosModelsPM = new List<ProyectoModel>();
            try
            {
                IEnumerable<Proyecto> proyectosFiltrados = repoProyectos.BuscarYFiltrarProyectos(datos.fechaDesde, datos.fechaHasta, datos.ci, datos.txtTitulo,
                                                                datos.txtDescripcion, datos.estado, datos.montoDado);
                if(proyectosFiltrados != null)
                {
                    foreach (Proyecto p in proyectosFiltrados)
                    {
                        ProyectoModel pM = new ProyectoModel()
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
                            Solicitante = p.Solicitante,
                            Tipo = p.Tipo
                        };
                        if (p.Tipo.Equals("P"))
                        {
                            ProyectoPersonal pP = p as ProyectoPersonal;
                            pM.ExpertisSolicitante = pP.ExpertisSolicitante;
                        }
                        else
                        {
                            ProyectoCooperativo pP = p as ProyectoCooperativo;
                            pM.CantIntegrantes = pP.CantIntegrantes;
                        }
                        /*OJOOOOOOO, TENER en cuenta que hay que generar esta vista otra vez y compilar
                         ya que el PROYECTOMODEL FUE MODIFICADO, Y AL RENDERIZAR LA VISTA HAY QUE PONER UN IF
                         PREGUNTANDO QUE TIPO ES, PARA QUE NO SALTE UNA EXCEPTION POR NULL
                         SI ES COOPERATIVO, EXPERTIS ES NULL, SI ES PERSONAL CANTINTEGRANTES ES NULL*/
                        proyectosFiltradosModelsPM.Add(pM);
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
            

            return Ok(proyectosFiltradosModelsPM);
        }


        [HttpGet]
        [Route("proyectos/financiar")]
        //[ResponseType(typeof(List<ProyectoModel>))]
        public IHttpActionResult Financiacion([FromUri] Models.FinancionModel datos)
        {
            if (datos != null)
            {
                Financiacion f = new Financiacion()
                {
                    ProyectoId = datos.ProyectoId,
                    CiInversor = datos.CiInversor,
                    MontoAFinanciar = datos.MontoAFinanciar,
                    FechaInversion = DateTime.Now.Date,
                };

                if (repoFinanciacion.Add(f))
                {
                    Proyecto proyActualizado = repoProyectos.FindById(datos.ProyectoId);
                    return Ok(proyActualizado);
                }
                else
                {
                    return InternalServerError();
                }
            }


            return InternalServerError();
        }


        [HttpGet]
        [Route("proyectos/inversiones")]
        //[ResponseType(typeof(List<ProyectoModel>))]
        public IHttpActionResult FindAll_X_CiInversor_Ordenado([FromUri] string CiInversor)
        {
            if (!String.IsNullOrEmpty(CiInversor))
            {

                IEnumerable<Financiacion> financiaciones = repoFinanciacion.FindAll_X_CiInversor_Ordenado(CiInversor);
                if (financiaciones != null)
                {
                    return Ok(financiaciones.Select(f => new FinancionModel
                    {
                        Id = f.Id,
                        ProyectoId = f.ProyectoId,
                        CiInversor = f.CiInversor,
                        MontoAFinanciar = f.MontoAFinanciar,
                        FechaInversion = f.FechaInversion,
                        proyectoModel = new ProyectoModel()
                        {
                            Id = f.Proyecto.Id,
                            Titulo = f.Proyecto.Titulo,
                            Descripcion = f.Proyecto.Descripcion,
                            Monto = f.Proyecto.Monto,
                            SaldoRestanteFinanciar = f.Proyecto.SaldoRestanteFinanciar,
                            CuotasAPagar = f.Proyecto.CuotasAPagar,
                            TasaInteresSegunCuota = f.Proyecto.TasaInteresSegunCuota,
                            MontoCuotaSinInt = f.Proyecto.MontoCuotaSinInt,
                            MontoTotalConIntereses = f.Proyecto.MontoTotalConIntereses,
                            MontoCuotaIntInc = f.Proyecto.MontoCuotaIntInc,
                            Imagen = f.Proyecto.Imagen,
                            FechaPresentacion = f.Proyecto.FechaPresentacion,
                            Estado = f.Proyecto.Estado,
                            Solicitante = f.Proyecto.Solicitante,
                            Tipo = f.Proyecto.Tipo
                        },
                    }));
                }else
                    return NotFound();
            }

            return BadRequest();
        }


        // PUT: api/proyectos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, Proyecto proyecto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != proyecto.Id)
            {
                return BadRequest();
            }

            db.Entry(proyecto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProyectoExiste(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/proyectos
        [ResponseType(typeof(Proyecto))]
        public IHttpActionResult Post(Proyecto Proyecto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Proyectos.Add(Proyecto);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Proyecto.Id }, Proyecto);
        }

        // DELETE: api/proyectos/5
        [ResponseType(typeof(Proyecto))]
        public IHttpActionResult Delete(int id)
        {
            Proyecto proyecto = db.Proyectos.Find(id);
            if (proyecto == null)
            {
                return NotFound();
            }

            db.Proyectos.Remove(proyecto);
            db.SaveChanges();

            return Ok(proyecto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProyectoExiste(int id)
        {
            return db.Proyectos.Count(e => e.Id == id) > 0;
        }
    }
}