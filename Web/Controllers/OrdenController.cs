﻿using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Utils;
using Web.ViewModel;

namespace Web.Controllers
{
    public class OrdenController : Controller
    {
        // GET: Orden
        public ActionResult Index()
        {
            IServiceCondicionOrden _ServiceCondicionOrden = new ServiceCondicionOrden();
            if (TempData.ContainsKey("NotificationMessage"))
            {
                ViewBag.NotificationMessage = TempData["NotificationMessage"];
            }
            ViewBag.CondicionOrdenID = _ServiceCondicionOrden.GetCondicionOrden();

            ViewBag.DetalleOrden = ViewModelCarrito.Instancia.Items;
            return View();
        }

        //Ordenar un producto y agregarlo al carrito
        public ActionResult ordenarProducto(int? ProductoID)
        {
            int cantidadProductos = ViewModelCarrito.Instancia.Items.Count();
            ViewBag.NotiCarrito = ViewModelCarrito.Instancia.AgregarItem((int)ProductoID);
            return PartialView("_OrdenCantidad");

        }
        //Actualizar Vista parcial detalle carrito
        private ActionResult DetalleCarrito()
        {

            return PartialView("_DetalleOrden", ViewModelCarrito.Instancia.Items);
        }

        //Actualizar cantidad de productos en el carrito
        public ActionResult actualizarCantidad(int productoID, int cantidad)
        {
            ViewBag.DetalleOrden = ViewModelCarrito.Instancia.Items;
            TempData["NotiCarrito"] = ViewModelCarrito.Instancia.SetItemCantidad(productoID, cantidad);
            TempData.Keep();
            return PartialView("_DetalleOrden", ViewModelCarrito.Instancia.Items);

        }
        //Actualizar solo la cantidad de productos que se muestra en el menú
        public ActionResult actualizarOrdenCantidad()
        {
            if (TempData.ContainsKey("NotiCarrito"))
            {
                ViewBag.NotiCarrito = TempData["NotiCarrito"];
            }
            int cantidadProductos = ViewModelCarrito.Instancia.Items.Count();
            return PartialView("_OrdenCantidad");

        }
        //Eliminar el producto de la orden
        public ActionResult eliminarProducto(int? productoID)
        {
            ViewBag.NotificationMessage = ViewModelCarrito.Instancia.EliminarItem((int)productoID);
            return PartialView("_DetalleOrden", ViewModelCarrito.Instancia.Items);
        }


        //Listado
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult IndexAdmin()
        {
            IEnumerable<Orden> lista = null;

            try
            {
                IServiceOrden _ServiceOrden = new ServiceOrden();
                lista = _ServiceOrden.GetOrden();
                IServiceCondicionOrden _ServiceCondicionOrden = new ServiceCondicionOrden();
                ViewBag.CondicionOrdenId = _ServiceCondicionOrden.GetCondicionOrden();
                return View(lista);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Orden";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Orden/Details/5
        public ActionResult Details(int? id)
        {
            ServiceOrden _ServiceOrden = new ServiceOrden();
            Orden orden = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }

                orden = _ServiceOrden.GetOrdenByID(id.Value);
                if (orden == null)
                {
                    TempData["Message"] = "No existe la orden solicitado";
                    TempData["Redirect"] = "Orden";
                    TempData["Redirect-Action"] = "IndexAdmin";
                    //TempData.Keep();
                    return RedirectToAction("Default", "Error");
                }
                return View(orden);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Orden";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Orden/Create
        //Guardar la orden
        [HttpPost]
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Save(Orden orden)
        {
            Detalle_Orden detalle = new Detalle_Orden();
            try
            {

                // Si no existe la sesión no hay nada
                if (ViewModelCarrito.Instancia.Items.Count() <= 0)
                {
                    // Validaciones de datos requeridos.
                    TempData["NotificationMessage"] = Utils.SweetAlertHelper.Mensaje("Orden", "Por favor seleccione los productos que desea ordenar", SweetAlertMessageType.warning);
                    return RedirectToAction("Index");
                }

                else
                {

                    var listaDetalle = ViewModelCarrito.Instancia.Items;
                    String localDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    orden.FechaCreacion = Convert.ToDateTime(localDate);
                    orden.IdCondicionOrden = 1;



                    foreach (var item in listaDetalle)
                    {
                        Detalle_Orden detalle_orden = new Detalle_Orden();
                        detalle_orden.IdOrden = item.ProductoId;
                        //detalle_orden.PrecioLinea = item.Precio;
                        detalle_orden.Cantidad = item.Cantidad;
                        detalle_orden.PrecioLinea = item.SubTotal;



                        //if (orden.TipoEntregaID == 1)
                        //{
                        //    orden.Envio = 4000;
                        //    orden.SubTotal = Carrito.Instancia.GetSubTotal() + orden.Envio;
                        //    orden.Impuesto = Carrito.Instancia.GetImpuesto();
                        //    orden.Total = (orden.Impuesto + orden.SubTotal);
                        //}
                        //else
                        //{
                        //    orden.SubTotal = Carrito.Instancia.GetSubTotal();
                        //    orden.Impuesto = Carrito.Instancia.GetImpuesto();
                        //    orden.Total = (orden.Impuesto + orden.SubTotal);
                        //}
                        orden.Detalle_Orden.Add(detalle_orden);
                    }
                }


                IServiceOrden _ServiceOrden = new ServiceOrden();

                Orden ordenSave = _ServiceOrden.Save(orden);

                // Limpia el Carrito de compras
                ViewModelCarrito.Instancia.eliminarViewModelCarrito();
                TempData["NotificationMessage"] = Utils.SweetAlertHelper.Mensaje("Orden", "¡Orden Guardada!", SweetAlertMessageType.success);
                // Reporte orden
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error  
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Orden";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        [HttpPost]
        // GET: Orden/Edit/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? id)
        {
            IServiceOrden _ServiceOrden = new ServiceOrden();
            Orden orden = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }

                orden = _ServiceOrden.GetOrdenByID(id.Value);
                //Asumo que es cuando la orden se procesa, en este caso se estaría actulizando a "Aprobado"
                //Falta revisar bien. Además, hay que implementar que se pueda rechazar la orden.
                orden.IdCondicionOrden = 2;
                _ServiceOrden.Save(orden);
                if (orden == null)
                {
                    TempData["Message"] = "No existe la orden solicitado";
                    TempData["Redirect"] = "Orden";
                    TempData["Redirect-Action"] = "IndexAdmin";
                    //TempData.Keep();
                    return RedirectToAction("Default", "Error");
                }
                return View("IndexAdmin");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Orden";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }


        //[HttpPost]

        //[CustomAuthorize((int)Roles.Vendedor)]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditConfirmed(int? id)
        //{
        //    IServiceOrden _ServiceOrden = new ServiceOrden();
        //    Orden orden = null;
        //    try
        //    {

        //        if (id == null)
        //        {
        //            return View();
        //        }
        //        orden.CondicionOrdenID = 2;
        //        _ServiceOrden.GetOrdenByID(id.Value);


        //        return RedirectToAction("IndexAdmin");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Salvar el error en un archivo 
        //        Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
        //        TempData["Message"] = "Error al procesar los datos! " + ex.Message;
        //        TempData.Keep();
        //        // Redireccion a la captura del Error
        //        return RedirectToAction("Default", "Error");
        //    }
        //}

        private SelectList listaCondicionOrden(int idCondicionOrden = 0)
        {
            //Lista de CondicionOrden
            IServiceCondicionOrden _ServiceCondicionOrden = new ServiceCondicionOrden();
            IEnumerable<CondicionOrden> listaCondicionOrden = _ServiceCondicionOrden.GetCondicionOrden();
            //return new SelectList(listaCondicionOrden, "CondicionOrdenID", "Nombre", idCondicionOrden);
            return new SelectList(listaCondicionOrden, "Id", "Descripcion", idCondicionOrden);

        }

        //private SelectList listaTipoEntrega(int idTipoEntrega = 0)
        //{
        //    //Lista de TipoEntrega
        //    IServiceTipoEntrega _ServiceTipoEntrega = new ServiceTipoEntrega();
        //    IEnumerable<TipoEntrega> listaTipoEntrega = _ServiceTipoEntrega.GetTipoEntrega();
        //    return new SelectList(listaTipoEntrega, "TipoEntregaID", "Nombre", idTipoEntrega);

        //}

        //private SelectList listaClientes()
        //{
        //    //Lista de Clientes
        //    IServiceCliente _ServiceCliente = new ServiceCliente();
        //    IEnumerable<Cliente> listaClientes = _ServiceCliente.GetCliente();

        //    return new SelectList(listaClientes, "ClienteID", "Nombre");
        //}

        //private SelectList listaPersonal()
        //{
        //    //Lista de Clientes
        //    IServiceUsuario _ServiceUsuario = new ServiceUsuario();
        //    IEnumerable<Usuario> listaUsuario = _ServiceUsuario.GetUsuario();

        //    return new SelectList(listaUsuario, "UsuarioID", "Nombre");
        //}
        //public ActionResult ObtenerOrdenesEnPreparacion()
        //{
        //    IEnumerable<Orden> lista = null;

        //    try
        //    {
        //        IServiceOrden _ServiceOrden = new ServiceOrden();
        //        lista = _ServiceOrden.GetOrdenEnPreparacion();


        //        return View("IndexAdmin", lista);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Salvar el error en un archivo 
        //        Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
        //        TempData["Message"] = "Error al procesar los datos! " + ex.Message;
        //        TempData["Redirect"] = "Orden";
        //        TempData["Redirect-Action"] = "Index";
        //        // Redireccion a la captura del Error
        //        return RedirectToAction("Default", "Error");
        //    }

        //}

        //public ActionResult ObtenerOrdenesFinalizadas()
        //{
        //    IEnumerable<Orden> lista = null;

        //    try
        //    {
        //        IServiceOrden _ServiceOrden = new ServiceOrden();
        //        lista = _ServiceOrden.GetOrdenFinalizada();


        //        return View("IndexAdmin", lista);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Salvar el error en un archivo 
        //        Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
        //        TempData["Message"] = "Error al procesar los datos! " + ex.Message;
        //        TempData["Redirect"] = "Orden";
        //        TempData["Redirect-Action"] = "Index";
        //        // Redireccion a la captura del Error
        //        return RedirectToAction("Default", "Error");
        //    }
        //}

        public ActionResult ordenByCondicionOrden(int? id)
        {
            IEnumerable<Orden> lista = null;
            IServiceOrden _ServiceOrden = new ServiceOrden();
            try
            {
                if (id != null)
                {
                    lista = _ServiceOrden.GetOrdenByCondicion((int)id);
                }


                return View("IndexAdmin", lista);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Orden";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }

    }
}