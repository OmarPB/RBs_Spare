using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.ViewModel;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        private static Empleado aux;
        public ActionResult Index()
        {


            return View();

        }

        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel empleado)
        {
            IServiceEmpleado _ServiceEmpleado = new ServiceEmpleado();
            try
            {
                //if (!ModelState.IsValid)
                //{
                Empleado oEmpleado = _ServiceEmpleado.Login(empleado.Id, empleado.Contrasenia);

                if (oEmpleado != null)
                    {
                        Session["User"] = oEmpleado;
                        Log.Info($"Accede {oEmpleado.Nombre} {oEmpleado.Apellidos} con el rol {oEmpleado.Rol.Id}-{oEmpleado.Rol.Descripcion}");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Log.Warn($"{empleado.Id} se intentó conectar  y falló");
                        TempData["Message"] = "Error al autenticarse";

                    }
                //}

                return View("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData.Keep();
                return RedirectToAction("Default", "Error");
            }
        }

        public ActionResult UnAuthorized()
        {
            try
            {
                ViewBag.Message = "No autorizado";

                if (Session["User"] != null)
                {
                    Empleado oEmpleado = Session["User"] as Empleado;
                    Log.Warn($"El Empleado {oEmpleado.Nombre} {oEmpleado.Apellidos} con el rol {oEmpleado.Rol.Id}-{oEmpleado.Rol.Descripcion}, intentó acceder una página sin derechos  ");
                }

                return View();
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData.Keep();
                return RedirectToAction("Default", "Error");
            }
        }


        public ActionResult Logout()
        {
            try
            {
                Log.Info("Se desconectó ");
                Session["User"] = null;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData.Keep();
                return RedirectToAction("Default", "Error");
            }
        }

        //Vista para solicitar al cliente los datos necesarios para recuperar la contraseña
        [HttpPost]
        public ActionResult IniciarRecuperacion(LoginViewModel empleado)
        {
            try
            {
                IServiceEmpleado service = new ServiceEmpleado();
                Empleado oEmpleado = service.VerificarEmpleado(empleado.Email);

                    if (oEmpleado != null)
                    {
                        service.Save(oEmpleado);
                    }
                
                return View("NotificacionCorreo");

            }
            catch (Exception ex)
            {

                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData.Keep();
                return RedirectToAction("Default", "Error");
            }
            
        }

        public ActionResult NotificacionCorreo()
        {
            return View();
        }


        [HttpGet]
        public ActionResult IniciarRecuperacion()
        {
            return View();
        }

        [HttpGet]
        //Vista para solicitar al cliente la nueva contraseña
        public ActionResult Recuperacion(string token)
        {
            IServiceEmpleado service = new ServiceEmpleado();
            try
            {
                if (token == null || token.Trim().Equals(""))
                {
                    return View("Index");
                }

                Empleado oEmpleado = service.GetEmpleadoByToken(token);
                if(oEmpleado == null)
                {
                    ViewBag.Error = "Tu token ha expirado";
                    return View("Index");
                }
                aux = oEmpleado;
                return View();
            }
            catch (Exception ex)
            {

                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData.Keep();
                return RedirectToAction("Default", "Error");
            }
            

            
        }

        [HttpPost]
        public ActionResult Recuperacion(Empleado empleado)
        {
            aux.Contrasenia = empleado.Contrasenia;
            IServiceEmpleado service = new ServiceEmpleado();
            try
            {
                if (aux != null)
                {
                    aux.TokenRecuperacion = null;
                    service.Save(aux);
                }
                return View("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData.Keep();
                return RedirectToAction("Default", "Error");
            }
        }
    }
}