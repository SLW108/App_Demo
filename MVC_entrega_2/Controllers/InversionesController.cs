using Dominio;
using MVC_entrega_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace MVC_entrega_2.Controllers
{
    public class InversionesController : Controller
    {
        private HttpClient cliente = new HttpClient();
        private HttpResponseMessage response = new HttpResponseMessage();
        private Uri proyectoUri = null;
        string errorAuthenticationMessage = "Necesitas estar autenticado para ver este recurso.";
        // GET: Inversiones

        public InversionesController()
        {
            cliente.BaseAddress = new Uri("https://localhost:44324/");
            proyectoUri = new Uri("https://localhost:44324/api");
            cliente.DefaultRequestHeaders.Accept.Clear();
            cliente.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public ActionResult Index()
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Login", "Home", new
                {
                    message = errorAuthenticationMessage,
                });
            }

            Usuario usuarioActivo = (Usuario)Session["usuario"];

            if (usuarioActivo.Rol.ToUpper() != "INVERSOR")
            {
                return RedirectToAction("Dashboard", "Home");
            };

            string ruta = $"{proyectoUri}/proyectos/inversiones?CiInversor={usuarioActivo.Ci}";

            Uri uri = new Uri(ruta);

            var response = cliente.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
            {
                var lista = response.Content.ReadAsAsync<IEnumerable<FinancionModel>>().Result;
                ViewBag.Mensaje = $"Se encontraron {lista.Count()} resultados";
                return View(lista);
            }
            else
            {
                ViewBag.Mensaje = $"Hubo un error, intente de nuevo";
                IEnumerable<FinancionModel> lista = new List<FinancionModel>();
                return View(lista);
            }
        }
        /*

        // GET: Inversiones/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Inversiones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inversiones/Create
        [HttpPost]
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
        }

        // GET: Inversiones/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Inversiones/Edit/5
        [HttpPost]
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
        }

        // GET: Inversiones/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Inversiones/Delete/5
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
