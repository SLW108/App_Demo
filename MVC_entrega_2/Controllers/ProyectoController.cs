using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Dominio;
using MVC_entrega_2.Models;
using MVC_entrega_2.ServicioDeArchivos;
using WCF;

namespace MVC_entrega_2.Controllers
{
    public class ProyectoController : Controller
    {
        private HttpClient cliente = new HttpClient();
        private HttpResponseMessage response = new HttpResponseMessage();
        private Uri proyectoUri = null;
        string errorAuthenticationMessage = "Necesitas estar autenticado para ver este recurso.";

        public ProyectoController()
        {
            cliente.BaseAddress = new Uri("https://localhost:44324/");
            proyectoUri = new Uri("https://localhost:44324/api");
            cliente.DefaultRequestHeaders.Accept.Clear();
            cliente.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Proyecto
        public ActionResult Index(string idInversion)
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Login", "Home", new
                {
                    message = errorAuthenticationMessage,
                });
            }

            ViewBag.ProyectoInversion = idInversion;

            Usuario usuarioActivo = (Usuario)Session["usuario"];

            string cedulaUsuario = usuarioActivo.Rol.ToUpper() != "SOLICITANTE" ? "" : usuarioActivo.Ci;

            string ruta = $"{proyectoUri}/proyectos/buscar/?&ci={cedulaUsuario}";

            Uri uri = new Uri(ruta);

            var response = cliente.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
            {
                var lista = response.Content.ReadAsAsync<IEnumerable<ProyectoBuscarModel>>().Result;
                ViewBag.Mensaje = $"Se encontraron {lista.Count()} resultados";
                return View(lista);
            }
            else
            {
                ViewBag.Mensaje = $"Hubo un error, intente de nuevo";
                IEnumerable<ProyectoBuscarModel> lista = new List<ProyectoBuscarModel>();
                return View(lista);
            }
        }

        // GET: Proyecto/Details/5
        public ActionResult Detail(int id)
        {

            if (Session["usuario"] == null)
            {
                return RedirectToAction("Login", "Home", new
                {
                    message = errorAuthenticationMessage,
                });
            }

            if (Session["errorInversionId"] != null && Session["errorInversionId"].ToString() != id.ToString())
            {
                Session["errorInversionId"] = null;
            }

            Inversor usuarioActivo = (Inversor)(Usuario)Session["usuario"];

            if (usuarioActivo.Rol.ToUpper() != "INVERSOR")
            {
                return RedirectToAction("Dashboard", "Home", new
                {
                    message = "No tienes permisos para ver el recurso solicitado",
                });
            }

            string ruta = $"{proyectoUri}/proyectos/{id}";

            Uri uri = new Uri(ruta);

            var response = cliente.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
            {
                var proyecto = response.Content.ReadAsAsync<ProyectoBaseModel>().Result;
                ViewBag.ProyectoTipo = proyecto.Tipo;
                ViewBag.ProyectoId = proyecto.Id;
                ViewBag.CiInversor = usuarioActivo.Ci;
                Session["proyectoId"] = proyecto.Id;
                ViewBag.MontoEstipulado = usuarioActivo.MontoEstipulado;
                ViewBag.ProyectoSaldoRestante = proyecto.SaldoRestanteFinanciar;
                return View(proyecto);
            }
            else
            {
                ViewBag.Mensaje = $"Hubo un error, intente de nuevo";
                IEnumerable<ProyectoBaseModel> lista = new List<ProyectoBaseModel>();
                return View(lista);
            }           
        }

        [HttpPost]
        public ActionResult Finaciacion(FormCollection datos)
        {

            if (Session["usuario"] == null)
            {
                return RedirectToAction("Login", "Home", new
                {
                    message = errorAuthenticationMessage,
                });
            }

            // RESETEO mensaje de error si lo hubiera
            Session["errorInversionId"] = null;

            Usuario usuarioInv = (Usuario)Session["usuario"];
            int proyectoId = (int)Session["proyectoId"];
            int montoMaxInversor = Int32.Parse(datos["MontoEstipulado"]);
            int saldoProyecto = Int32.Parse(datos["ProyectoSaldoRestante"]);
            int inversionSolicitada = Int32.Parse(datos["MontoAFinanciar"]);

            if (inversionSolicitada <= saldoProyecto && inversionSolicitada <= montoMaxInversor)
            {
                string ruta = $"{proyectoUri}/proyectos/financiar?ProyectoId={proyectoId}&CiInversor={usuarioInv.Ci}&MontoAFinanciar={inversionSolicitada}";

                Uri uri = new Uri(ruta);

                var response = cliente.GetAsync(uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    var proyecto = response.Content.ReadAsAsync<ProyectoBaseModel>().Result;
                    return RedirectToAction("Index", "Proyecto", new { idInversion = proyectoId.ToString() });
                }
                else
                {
                    ViewBag.Mensaje = $"Hubo un error, intente de nuevo";
                    return RedirectToAction("Login", "Home");
                }
            } else
            {
                Session["errorInversionId"] = proyectoId;
                return RedirectToAction("Detail", "Proyecto", new { id = proyectoId });
            }

            
        }

        [HttpPost]
        public ActionResult Index(Models.ProyectoFormBuscarModel dato)
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Login", "Home", new
                {
                    message = errorAuthenticationMessage,
                });
            }

            Usuario usuarioActivo = (Usuario)Session["usuario"];

            string cedulaUsuario = usuarioActivo.Rol.ToUpper() != "SOLICITANTE" ? dato.ci : usuarioActivo.Ci;

            string ruta = $"{proyectoUri}/proyectos/buscar/?txtTitulo={dato.txtTitulo}&txtDescripcion={dato.txtDescripcion}&ci={cedulaUsuario}" +
                $"&montoDado={dato.montoDado}&estado={dato.estado}&fechaDesde={dato.fechaDesde}&fechaHasta={dato.fechaHasta}";
            Uri uri = new Uri(ruta);
            ViewBag.ruta = ruta;

            var response = cliente.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
            {
                var lista = response.Content.ReadAsAsync<IEnumerable<ProyectoBuscarModel>>().Result;
                ViewBag.Mensaje = $"Se encontraron {lista.Count()} resultados";
                return View(lista);
            } else
            {
                ViewBag.Mensaje = $"Hubo un error, intente de nuevo";
                IEnumerable<ProyectoBuscarModel> lista = new List<ProyectoBuscarModel>();
                return View(lista);
            }
        }

        // GET: Proyecto
        public ActionResult Importados(string feedbackMessage)
        {

            ServicioDeArchivosClient proxy = new ServicioDeArchivosClient();
            proxy.Open();
            IEnumerable<ProyectoDTO> lista = proxy.FindAllProyectos();
            proxy.Close();
            ViewBag.feedbackMessage = feedbackMessage != null ? feedbackMessage : null; 
            return View(lista);
        }

        // GET: Proyecto/Create
        /* public ActionResult Create()
         {
             return View();
         }*/

        // POST: Proyecto/Create
        /*[HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }*/


        // GET: Proyecto/Edit/5
        /*        public ActionResult Edit(int id)
                {
                    return View();
                }*/

        // POST: Proyecto/Edit/5
        /*[HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }*/

        // GET: Proyecto/Delete/5
        /*public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Proyecto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }*/
    }
}
