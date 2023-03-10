using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{
    public class EmpleadoController : Controller
    {
        private static String Action;

        // Significa  que solo los que tienen el rol de Administrador pueden accederla 
        // ver Enums.cs  
        // public enum Roles { Administrador = 1, Procesos = 2, Reportes = 3}
        [CustomAuthorize((int)Roles.Administrador)]
        // GET: Empleado
        public ActionResult Index()
        {
            try
            {
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                //Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData.Keep();
                return RedirectToAction("Default", "Error");
            }
        }

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult List()
        {
            IEnumerable<Empleado> lista = null;
            try
            {
                //Log.Info("Visita");

                if (!String.IsNullOrEmpty(Action))
                {
                    ViewBag.Action = Action;
                }

                IServiceEmpleado _ServiceEmpleado = new ServiceEmpleado();
                lista = _ServiceEmpleado.GetEmpleado();
                Action = "";
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                //Log.Error(ex, MethodBase.GetCurrentMethod());

                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

            return View(lista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Save(Empleado Empleado)
        {
            string errores = "";
            try
            {
                // Es valido
                if (ModelState.IsValid)
                {
                    ServiceEmpleado _ServiceEmpleado = new ServiceEmpleado();
                    //Se crea una bitácora con los datos iniciales
                    var empSession = Session["User"] as Empleado;
                    BitacoraEmpleados bitacora = new BitacoraEmpleados();
                    bitacora.IdEmpleadoEjecutor = empSession.Id;
                    bitacora.NombreEmpleadoEjecutor = empSession.Nombre + " " + empSession.Apellidos;
                    bitacora.FechaCambios = DateTime.Now;

                    _ServiceEmpleado.Save(Empleado, bitacora);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);

                    TempData["Message"] = "Error al procesar los datos! " + errores;
                    TempData.Keep();

                    return View("Create", Empleado);
                }

                Action = "S";

                // redirigir
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                //Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }


        //GET: Empleado/Details/    
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult AjaxFilterDetails(int? id)
        {
            ServiceEmpleado _ServiceEmpleado = new ServiceEmpleado();
            Empleado empleado = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                empleado = _ServiceEmpleado.GetEmpleadoByID(id.Value);
                //var detalles = new List<Empleado>();
                //detalles.Add(Empleado);

                return PartialView("_PartialViewDetailsEmpleado", empleado);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                //Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        //public ActionResult AjaxFilterDetails5(int id)
        //{
        //    IServiceEmpleado serviceEmpleado = new ServiceEmpleado();
        //    Empleado empleado = serviceEmpleado.GetEmpleadoByID(id);

        //    var detalles = new List<Empleado>();
        //    detalles.Add(empleado);
        //    return PartialView("_PartialViewDetailsEmpleado", detalles);
        //}

        // GET: Empleado/Edit/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? id)
        {
            IServiceEmpleado _ServiceEmpleado = new ServiceEmpleado();
            Empleado Empleado = null;

            //ViewBag con los tipos de Empleado
            IServiceRol serviceRol = new ServiceRol();
            ViewBag.ListaTipos = serviceRol.GetRol();

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                Empleado = _ServiceEmpleado.GetEmpleadoByID(id.Value);
                // Response.StatusCode = 500;

                Action = "U";

                return PartialView("_EditPartialView", Empleado);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                //Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }


        // GET: Empleado/Create
        //[CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Create()
        {
            //ViewBag con los tipos de Empleado
            IServiceRol serviceRol = new ServiceRol();
            ViewBag.ListaTipos = serviceRol.GetRol();
            Empleado empleado = new Empleado();

            return PartialView("_CreatePartialView", empleado);
            //return View();
        }


        // GET: Empleado/Delete/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Delete(int? id)
        {
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                ServiceEmpleado _ServiceEmpleado = new ServiceEmpleado();
                Empleado Empleado = _ServiceEmpleado.GetEmpleadoByID(id.Value);

                Action = "D";

                return PartialView("_DeletePartialView", Empleado);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                //Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult DeleteConfirmed(int? id)
        {
            ServiceEmpleado _ServiceEmpleado = new ServiceEmpleado();

            try
            {

                if (id == null)
                {
                    return View();
                }

                //Se crea la bitácora
                var empSession = Session["User"] as Empleado;
                BitacoraEmpleados bitacora = new BitacoraEmpleados();
                bitacora.IdEmpleadoEjecutor = empSession.Id;
                bitacora.NombreEmpleadoEjecutor = empSession.Nombre + " " + empSession.Apellidos;
                bitacora.FechaCambios = DateTime.Now;

                _ServiceEmpleado.DeleteEmpleado(id.Value, bitacora);

                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                //Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        
        //Fin
    }
}