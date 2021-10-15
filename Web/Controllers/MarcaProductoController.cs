using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{
    public class MarcaProductoController : Controller
    {
        private static String Action;

        // Significa  que solo los que tienen el rol de Administrador pueden accederla 
        // ver Enums.cs  
        // public enum Roles { Administrador = 1, Procesos = 2, Reportes = 3}
                [CustomAuthorize((int)Roles.Administrador)]
        // GET: MarcaProducto
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
            IEnumerable<MarcaProducto> lista = null;
            try
            {
                //Log.Info("Visita");

                if (!String.IsNullOrEmpty(Action))
                {
                    ViewBag.Action = Action;
                }

                IServiceMarcaProducto _ServiceMarcaProducto = new ServiceMarcaProducto();
                lista = _ServiceMarcaProducto.GetMarcaProducto();
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
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Save(MarcaProducto MarcaProducto)
        {
            string errores = "";
            try
            {
                // Es valido
                if (ModelState.IsValid)
                {
                    ServiceMarcaProducto _ServiceMarcaProducto = new ServiceMarcaProducto();
                    _ServiceMarcaProducto.Save(MarcaProducto);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);

                    TempData["Message"] = "Error al procesar los datos! " + errores;
                    TempData.Keep();

                    return PartialView("Create", MarcaProducto);
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


        //GET: MarcaProducto/Details/    
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult AjaxFilterDetails(int? id)
        {
            ServiceMarcaProducto _ServiceMarcaProducto = new ServiceMarcaProducto();
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                MarcaProducto MarcaProducto = _ServiceMarcaProducto.GetMarcaProductoByID(id.Value);
                var detalles = new List<MarcaProducto>
                {
                    MarcaProducto
                };

                return PartialView("_PartialViewDetailsMarcaProducto", detalles);
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
        //    IServiceMarcaProducto serviceMarcaProducto = new ServiceMarcaProducto();
        //    MarcaProducto MarcaProducto = serviceMarcaProducto.GetMarcaProductoByID(id);

        //    var detalles = new List<MarcaProducto>();
        //    detalles.Add(MarcaProducto);
        //    return PartialView("_PartialViewDetailsMarcaProducto", detalles);
        //}

        // GET: MarcaProducto/Edit/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? id)
        {
            IServiceMarcaProducto _ServiceMarcaProducto = new ServiceMarcaProducto();
            MarcaProducto MarcaProducto = null;


            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                MarcaProducto = _ServiceMarcaProducto.GetMarcaProductoByID(id.Value);
                // Response.StatusCode = 500;

                Action = "U";

                return PartialView("_EditPartialView", MarcaProducto);
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


        // GET: MarcaProducto/Create
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Create()
        {
            //ViewBag con los tipos de MarcaProducto
            IServiceRol serviceRol = new ServiceRol();
            MarcaProducto MarcaProducto = new MarcaProducto();

            return PartialView("_CreatePartialView", MarcaProducto);
            //return View();
        }


        // GET: MarcaProducto/Delete/5
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

                ServiceMarcaProducto _ServiceMarcaProducto = new ServiceMarcaProducto();
                MarcaProducto MarcaProducto = _ServiceMarcaProducto.GetMarcaProductoByID(id.Value);

                Action = "D";

                return PartialView("_DeletePartialView", MarcaProducto);
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
            ServiceMarcaProducto _ServiceMarcaProducto = new ServiceMarcaProducto();

            try
            {

                if (id == null)
                {
                    return View();
                }

                _ServiceMarcaProducto.DeleteMarcaProducto(id.Value);

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
    }
}