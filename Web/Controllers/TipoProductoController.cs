using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class TipoProductoController : Controller
    {
        private static String Action;

        // Significa  que solo los que tienen el rol de Administrador pueden accederla 
        // ver Enums.cs  
        // public enum Roles { Administrador = 1, Procesos = 2, Reportes = 3}
        //[CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        // GET: TipoProducto
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

        //[CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult List()
        {
            IEnumerable<TipoProducto> lista = null;
            try
            {
                //Log.Info("Visita");

                if (!String.IsNullOrEmpty(Action))
                {
                    ViewBag.Action = Action;
                }

                IServiceTipoProducto _ServiceTipoProducto = new ServiceTipoProducto();
                lista = _ServiceTipoProducto.GetTipoProducto();
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
        //[CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult Save(TipoProducto tipoProducto)
        {
            string errores = "";
            try
            {
                // Es valido
                if (ModelState.IsValid)
                {
                    ServiceTipoProducto _ServiceTipoProducto = new ServiceTipoProducto();
                    _ServiceTipoProducto.Save(tipoProducto);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);

                    TempData["Message"] = "Error al procesar los datos! " + errores;
                    TempData.Keep();

                    return View("Create", tipoProducto);
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


        //GET: TipoProducto/Details/    
        //[CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult AjaxFilterDetails(int? id)
        {
            ServiceTipoProducto _ServiceTipoProducto = new ServiceTipoProducto();
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                TipoProducto tipoProducto = _ServiceTipoProducto.GetTipoProductoByID(id.Value);
                var detalles = new List<TipoProducto>
                {
                    tipoProducto
                };

                return PartialView("_PartialViewDetailsTipoProducto", detalles);
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
        //    IServiceTipoProducto serviceTipoProducto = new ServiceTipoProducto();
        //    TipoProducto TipoProducto = serviceTipoProducto.GetTipoProductoByID(id);

        //    var detalles = new List<TipoProducto>();
        //    detalles.Add(TipoProducto);
        //    return PartialView("_PartialViewDetailsTipoProducto", detalles);
        //}

        // GET: TipoProducto/Edit/5
        //[CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult Edit(int? id)
        {
            IServiceTipoProducto _ServiceTipoProducto = new ServiceTipoProducto();
            TipoProducto tipoProducto = null;


            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                tipoProducto = _ServiceTipoProducto.GetTipoProductoByID(id.Value);
                // Response.StatusCode = 500;

                Action = "U";

                return PartialView("_EditPartialView", tipoProducto);
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


        // GET: TipoProducto/Create
        //[CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult Create()
        {
            //ViewBag con los tipos de TipoProducto
            IServiceRol serviceRol = new ServiceRol();
            TipoProducto tipoProducto = new TipoProducto();

            return PartialView("Create", tipoProducto);
            //return View();
        }


        // GET: TipoProducto/Delete/5
        //[CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult Delete(int? id)
        {
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                ServiceTipoProducto _ServiceTipoProducto = new ServiceTipoProducto();
                TipoProducto tipoProducto = _ServiceTipoProducto.GetTipoProductoByID(id.Value);

                Action = "D";

                return PartialView("_DeletePartialView", tipoProducto);
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
        //[CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult DeleteConfirmed(int? id)
        {
            ServiceTipoProducto _ServiceTipoProducto = new ServiceTipoProducto();

            try
            {

                if (id == null)
                {
                    return View();
                }

                _ServiceTipoProducto.DeleteTipoProducto(id.Value);

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