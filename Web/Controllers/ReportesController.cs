using ApplicationCore.Services;
using Infraestructure.Models;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Web.Security;
using Web.ViewModel;
using Table = iText.Layout.Element.Table;

namespace Web.Controllers
{
    public class ReportesController : Controller
    {
        static IEnumerable<Orden> listaOrdenesFiltro = null;
        static IEnumerable<Cita> listaCitasFiltro = null;
        static string mesOrdenes;
        static string mesCitas;

        // GET: Reportes
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            return View();
        }

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult ReporteOrdenes() //AntesCierreDep
        {
            try
            {
                return View();
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

        public ActionResult AjaxReporteOrdenes(ViewModelParametro parametro)
        {
            IEnumerable<Orden> lista = null;
            try
            {

                IServiceOrden _ServiceOrden = new ServiceOrden();
                lista = _ServiceOrden.GetOrdenByFecha(parametro.Fecha);
                //Llena la lista para el PDF
                listaOrdenesFiltro = lista;
                //Convierte en letra el mes consultado para mostrarlo en el pdf
                mesOrdenes = parametro.Fecha.ToString("MMMM yyyy");
                return PartialView("_ReporteOrdenes", lista);
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

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult ReporteCitas() //AntesCierreDep
        {
            try
            {
                return View();
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

        public ActionResult AjaxReporteCitas(ViewModelParametro parametro)
        {
            IEnumerable<Cita> lista = null;
            try
            {

                IServiceCita _ServiceCita = new ServiceCita();
                lista = _ServiceCita.GetCitasByFecha(parametro.Fecha);
                //Llena la lista para el PDF
                listaCitasFiltro = lista;
                //Convierte en letra el mes consultado para mostrarlo en el pdf
                mesCitas = parametro.Fecha.ToString("MMMM yyyy");
                return PartialView("_ReporteCitas", lista);
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


        //Creación de PDF de Órdenes con Rotativa
        public ActionResult createPdfOrdenCatalogo()
        {
            ViewBag.mesOrdenes = mesOrdenes;
            return new PartialViewAsPdf("PdfOrdenes", listaOrdenesFiltro)
            {
                FileName = "Reporte Órdenes " + mesOrdenes + ".pdf",
                PageSize = Rotativa.Options.Size.A4,
                PageMargins = new Rotativa.Options.Margins(10, 10, 20, 10),
                CustomSwitches = "--page-offset 0 --footer-right [page] --footer-font-size 10"
            };
        }

        //Creación de PDF de Citas con Rotativa
        public ActionResult createPdfCitas()
        {
            ViewBag.mesCitas = mesCitas;
            return new PartialViewAsPdf("PdfCitas", listaCitasFiltro)
            {
                FileName = "Reporte Citas " + mesCitas + ".pdf",
                PageSize = Rotativa.Options.Size.A4,
                PageMargins = new Rotativa.Options.Margins(10, 10, 20, 10),
                CustomSwitches = "--page-offset 0 --footer-right [page] --footer-font-size 10"
            };
        }

        //Fin
    }
}