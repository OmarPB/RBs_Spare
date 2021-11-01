using ApplicationCore.Services;
using Infraestructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{
    public class ProductoController : Controller
    {
        private static String Action;

        // Significa  que solo los que tienen el rol de Administrador pueden accederla 
        // ver Enums.cs  
        // public enum Roles { Administrador = 1, Procesos = 2, Reportes = 3}
        //[CustomAuthorize((int)Roles.Administrador)]
        // GET: Producto
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData.Keep();
                return RedirectToAction("Default", "Error");
            }
        }

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult List()
        {
            IEnumerable<Producto> lista = null;
            try
            {
                Log.Info("Visita");

                if (!String.IsNullOrEmpty(Action))
                {
                    ViewBag.Action = Action;
                }

                IServiceProducto _ServiceProducto = new ServiceProducto();
                lista = _ServiceProducto.GetProducto();
                Action = "";
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());

                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

            return View(lista);
        }

        public ActionResult Catalogo()
        {
            IEnumerable<Producto> lista = null;
            try
            {
                Log.Info("Visita");

                if (!String.IsNullOrEmpty(Action))
                {
                    ViewBag.Action = Action;
                }

                IServiceProducto _ServiceProducto = new ServiceProducto();
                lista = _ServiceProducto.GetProducto();
                Action = "";
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());

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
        public ActionResult Save(Producto Producto, HttpPostedFileBase ImageFile)
        {
            string errores = "";
            MemoryStream target = new MemoryStream();

            //Creación de los ViewBag
            IServiceTipoProducto serviceTipoProducto = new ServiceTipoProducto();
            IServiceMarcaProducto serviceMarcaProducto = new ServiceMarcaProducto();

            ViewBag.ListaTipos = serviceTipoProducto.GetTipoProducto();
            ViewBag.ListaMarcas = serviceMarcaProducto.GetMarcaProducto();

            //Verifica que exista la marca
            //Instancio ServiceMarca
            //IServiceMarcaProducto serviceMarcaProducto = new ServiceMarcaProducto();

            //Verifico que exista una marca con el IdMarcaEnviado
            //MarcaProducto marcaProducto = serviceMarcaProducto.GetMarcaProductoByID(Convert.ToInt32(Producto.IdMarca));
            //if (marcaProducto == null)
            //{
            //    TempData["Message"] = "¡La Marca seleccionada no existe!";
            //    TempData.Keep();

            //    //Creación de los ViewBag
            //    IServiceTipoProducto serviceTipoProducto = new ServiceTipoProducto();
            //    IServiceMarcaProducto serviceMarcaProducto = new ServiceMarcaProducto();

            //    ViewBag.ListaTipos = serviceTipoProducto.GetTipoProducto();
            //    ViewBag.ListaMarcas = serviceMarcaProducto.GetMarcaProducto();

            //    return View("Create", Producto);
            //}

            try
            {
                // Cuando es Insert Image viene en null porque se pasa diferente
                if (Producto.Imagen == null)
                {
                    if (ImageFile != null)
                    {
                        ImageFile.InputStream.CopyTo(target);
                        Producto.Imagen = target.ToArray();
                        ModelState.Remove("Imagen");
                    }
                    else
                    {
                        IServiceProducto serviceProducto = new ServiceProducto();
                        Producto.Imagen = serviceProducto.GetProductoByID(Producto.Id).Imagen;
                    }
                }

                //if (Producto.FotoFactura == null)
                //{
                //    if (ImageFile2 != null)
                //    {
                //        ImageFile2.InputStream.CopyTo(target2);
                //        Producto.FotoFactura = target2.ToArray();
                //        ModelState.Remove("FotoFactura");
                //    }
                //    else
                //    {
                //        IServiceProducto serviceProducto = new ServiceProducto();
                //        Producto.FotoFactura = serviceProducto.GetProductoByID(Producto.Id).FotoFactura;
                //    }
                //}

                // Es valido
                if (ModelState.IsValid)
                {
                    ServiceProducto _ServiceProducto = new ServiceProducto();
                    _ServiceProducto.Save(Producto);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);

                    TempData["Message"] = "Error al procesar los datos! " + errores;
                    TempData.Keep();

                    return View("Create", Producto);
                }

                Action = "S";
                // redirigir
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }


        // GET: Producto/Details/5      
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult AjaxFilterDetails(int? id)
        {
            ServiceProducto _ServiceProducto = new ServiceProducto();
            Producto Producto = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                Producto = _ServiceProducto.GetProductoByID(id.Value);

                return PartialView("_PartialViewDetailsProducto", Producto);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Producto/Edit/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? id)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            Producto Producto = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                Producto = _ServiceProducto.GetProductoByID(id.Value);
                // Response.StatusCode = 500;

                //Creación de los ViewBag
                IServiceTipoProducto serviceTipoProducto = new ServiceTipoProducto();
                IServiceMarcaProducto serviceMarcaProducto = new ServiceMarcaProducto();

                ViewBag.ListaTipos = serviceTipoProducto.GetTipoProducto();
                ViewBag.ListaMarcas = serviceMarcaProducto.GetMarcaProducto();

                Action = "U";

                return PartialView("_EditPartialView", Producto);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }


        // GET: Producto/Create
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Create()
        {
            //Declaro instacias de los service para cada una de las clases de las que se extraerá una lista para el viewbag
            IServiceTipoProducto serviceTipoProducto = new ServiceTipoProducto();
            IServiceMarcaProducto serviceMarcaProducto = new ServiceMarcaProducto();

            ViewBag.ListaTipos = serviceTipoProducto.GetTipoProducto();
            ViewBag.ListaMarcas = serviceMarcaProducto.GetMarcaProducto();

            Producto producto = new Producto();

            return PartialView("_CreatePartialView", producto);
        }


        // GET: Producto/Delete/5
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

                ServiceProducto _ServiceProducto = new ServiceProducto();
                Producto Producto = _ServiceProducto.GetProductoByID(id.Value);

                //Para el toast
                Action = "D";

                return PartialView("_DeletePartialView", Producto);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
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
            ServiceProducto _ServiceProducto = new ServiceProducto();

            try
            {

                if (id == null)
                {
                    return View();
                }

                _ServiceProducto.DeleteProducto(id.Value);

                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        //public ContentResult GetProductoByName(string name)
        //{
        //    IServiceProducto serviceProducto = new ServiceProducto();
        //    var lista = serviceProducto.GetProductoByName(name).ToList();
        //    var settings = new JsonSerializerSettings()
        //    {
        //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        //        Error = (sender, args) =>
        //        {
        //            args.ErrorContext.Handled = true;
        //        },
        //    };
        //    string json = JsonConvert.SerializeObject(lista, settings);

        //    return Content(json);
        //}

        //Fin
    }
}