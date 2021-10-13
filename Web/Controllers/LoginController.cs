using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {


            return View();

        }

        [ValidateAntiForgeryToken]
        public ActionResult Login(Empleado empleado)
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
                ViewBag.Message = "Un Authorized Page!";

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
        public ActionResult IniciarRecuperacion(Empleado empleado)
        {
            try
            {
                IServiceEmpleado service = new ServiceEmpleado();

                if(service.VerificarEmpleado(empleado.Email)){

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


        [HttpGet]
        public ActionResult IniciarRecuperacion()
        {
            return View();
        }

        //Vista para solicitar al cliente la nueva contraseña
        public ActionResult Recuperacion()
        {
            return View();
        }
    }
}