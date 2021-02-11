using System;
using MVC_entrega_2.ServicioDeArchivos;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio;
using Datos;
using WCF;
using MVC_entrega_2.Models;

namespace MVC_entrega_2.Controllers
{
    public class HomeController : Controller
    {
        static RepoUsuario repoUsuario = new RepoUsuario();
        static RepoInversor repoInversor = new RepoInversor();
        string errorAuthenticationMessage = "Necesitas estar autenticado para ver este recurso.";

        public ActionResult Index(string feedbackMessage)
        {
            if (feedbackMessage != null)
            {
                ViewBag.feedbackMessage = feedbackMessage;
            }
            return View();
        }

        public ActionResult Register(string message)
        {
            if (Session["usuario"] != null)
            {
                return RedirectToAction("Dashboard");
            }
            ViewBag.errorMessage = message;
            return View();
        }

        [HttpPost]
        public ActionResult Register(FormCollection collection)
        {
            try
            {
                string usuarioCedula = collection["Ci"];
                Usuario usuarioForm = repoUsuario.FindByCi(usuarioCedula);

                if (usuarioForm != null)
                {
                    string message = "El Usuario ya existe en nuestra base de datos";
                    return RedirectToAction("Login", "Home", new
                    {
                        message,
                    });
                } else
                {
                    if (collection["Password"] == collection["PasswordConfirmar"])
                    {
                        string message = "";

                        Inversor nuevoInversor = new Inversor()
                        {
                            Id = Convert.ToInt32(collection["Ci"]),
                            Ci = collection["Ci"],
                            Nombre = collection["Nombre"],
                            Apellido = collection["Apellido"],
                            Password = collection["Password"],
                            Presentacion = collection["Presentacion"],
                            FechaNacimiento = DateTime.Parse(collection["FechaNacimiento"]),
                            Celular = collection["Celular"],
                            Email = collection["Email"],
                            MontoEstipulado = Int32.Parse(collection["MontoEstipulado"]),
                            Rol = "Inversor"
                        };


                        // FALTA AÑADIR VALIDACIONES de usuario
                        if ( nuevoInversor.IsValidUser() )
                        {
                            if (repoInversor.Add(nuevoInversor))
                            {
                                Session["usuario"] = nuevoInversor;
                                return RedirectToAction("Dashboard");
                            }
                            else
                            {
                                message = "Hubo un problma al ingresar";
                                return RedirectToAction("Login", "Home", new
                                {
                                    message,
                                });
                            }
                        }
                        message = "Datos no válidos";
                        return RedirectToAction("Login", "Home", new
                        {
                            message,
                        });
                    }
                    else
                    {
                        string message = "Contraseñas no son iguales";
                        return RedirectToAction("Login", "Home", new
                        {
                            message,
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult Login(string message)
        {
            if (Session["usuario"] != null)
            {
                Session["usuario"] = null;
            }
            if (message != null)
            {
                ViewBag.errorMessage = message;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            try
            {
                string usuarioCedula = collection["Ci"];

                Inversor usuarioInversor = null;
                Solicitante usuarioSolicitante = null;
                Usuario usuarioForm = repoUsuario.FindByCi(usuarioCedula);//Ojo aca

                if (usuarioForm != null && usuarioForm.Ci != null)
                {
                    if (usuarioForm.Password == collection["Password"])
                    {

                        if (usuarioForm.Rol.ToUpper() == "INVERSOR")
                        {
                            usuarioInversor = (Inversor)usuarioForm;
                        }
                        else
                        {
                            usuarioSolicitante = (Solicitante)usuarioForm;
                        }

                        if (usuarioInversor != null)
                        {
                            Session["usuario"] = usuarioInversor;
                        } else
                        {
                            Session["usuario"] = usuarioSolicitante;
                        }
                        return RedirectToAction("Dashboard");
                    }
                    else
                    {
                        string message = "Contraseña incorrecta";
                        return RedirectToAction("Login", "Home", new
                        {
                            message,
                        });
                    }
                }
                else
                {
                    string message = "Usuario no válido";
                    return RedirectToAction("Login", "Home", new
                    {
                        message,
                    });
                }
            }
            catch /*(Exception ex)*/
            {
                return View();
            }
        }

        public ActionResult Dashboard(string feedbackMessage)
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Login", "Home", new
                {
                    message = errorAuthenticationMessage,
                });
            }
            ViewBag.errorMessage = feedbackMessage;
            ViewBag.usuarioActual = (Usuario)Session["usuario"];
            return View();
        }
        
        public ActionResult ImportarUsuarios()
        {
            string feedbackMessage;
            try
            {
                ServicioDeArchivosClient proxy = new ServicioDeArchivosClient();
                proxy.Open();
                if (proxy.cargarUsuarios("usuarios.txt", "usuario"))
                {
                    proxy.Close();
                    feedbackMessage = "Se importaron correctamente los Usuarios";
                    return RedirectToAction("Importados", "Usuario", new { feedbackMessage });
                }
                feedbackMessage = "Hubo un error con la solicitud";

            }
            catch (Exception ex)
            {
                feedbackMessage = "Hubo un error al importar, intente nuevamente " + ex.Message.ToString();

            }
            return RedirectToAction("Index", "Home", new { feedbackMessage });
        }

        public ActionResult ImportarProyectos()
        {
            string feedbackMessage = "";
            try
                {
                    ServicioDeArchivosClient proxy = new ServicioDeArchivosClient();
                    proxy.Open();
                if (proxy.FindAllSolicitantes().Count() > 0)
                {
                    if (proxy.cargarUsuarios("proyectos.txt", "proyecto"))
                    {
                        feedbackMessage = "Se importaron correctamente los Proyectos";
                        return RedirectToAction("Importados", "Proyecto", new { feedbackMessage });
                    }
                    feedbackMessage = "Hubo un error con la solicitud";
                }
                else
                {
                    feedbackMessage = "Por favor, primero procesa la importación de Usuarios";
                }
                proxy.Close();
                }
                catch (Exception ex)
                {
                    feedbackMessage = "Hubo un error al importar, intente nuevamente " + ex.Message.ToString();

                }
                return RedirectToAction("Index", "Home", new { feedbackMessage });
            /*}*/
        }
    }
}