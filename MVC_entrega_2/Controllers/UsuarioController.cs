using Dominio;
using MVC_entrega_2.Models;
using MVC_entrega_2.ServicioDeArchivos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using WCF;

namespace MVC_entrega_2.Controllers
{
    public class UsuarioController : Controller
    {
        public ActionResult Importados(string feedbackMessage)
        {
                ServicioDeArchivosClient proxy = new ServicioDeArchivosClient();
                proxy.Open();
                IEnumerable<SolicitanteDTO> lista = proxy.FindAllSolicitantes();
                proxy.Close();
                ViewBag.feedbackMessage = feedbackMessage != null ? feedbackMessage : null;
                return View(lista);
        }
    }
}
