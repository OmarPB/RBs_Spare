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
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.ViewModel;

namespace Web.Controllers
{
    public class ReportesController : Controller
    {
        // GET: Reportes
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
                Console.Write(lista.ToString());
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

        ///////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////// Método que crea el PDF ///////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////
        public ActionResult CreatePdfOrdenCatalogo()
        {
            IEnumerable<Orden> lista = null;
            try
            {
                // Extraer informacion
                IServiceOrden _ServiceOrden = new ServiceOrden();
                lista = _ServiceOrden.GetOrden();

                // Crear stream para almacenar en memoria el reporte 
                MemoryStream ms = new MemoryStream();
                //Initialize writer
                PdfWriter writer = new PdfWriter(ms);

                //Initialize document
                PdfDocument pdfDoc = new PdfDocument(writer);
                Document doc = new Document(pdfDoc, iText.Kernel.Geom.PageSize.A1, false);

                DateTime fecha = (DateTime)lista.First().FechaCreacion;
                string mes = fecha.ToString("MMMM");

                Paragraph header = new Paragraph("Órdenes de " + mes)
                                   .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                                   .SetFontSize(14)
                                   .SetFontColor(ColorConstants.BLUE);
                doc.Add(header);


                // Crear tabla con 6 columnas 
                Table table = new Table(6, true);


                // los Encabezados
                table.AddHeaderCell("Id");
                table.AddHeaderCell("Cliente");
                table.AddHeaderCell("Fecha");
                table.AddHeaderCell("Subtotal");
                table.AddHeaderCell("Total Impuesto");
                table.AddHeaderCell("Total Final");


                foreach (var item in lista)
                {

                    // Agregar datos a las celdas
                    table.AddCell(new Paragraph(item.Id.ToString()));
                    table.AddCell(new Paragraph(item.NombreCliente.ToString() + item.ApellidosCliente.ToString()));
                    table.AddCell(new Paragraph(item.FechaCreacion.ToString()));
                    table.AddCell(new Paragraph(item.Subtotal.ToString()));
                    table.AddCell(new Paragraph(item.TotalIVA.ToString()));
                    table.AddCell(new Paragraph(item.TotalFinal.ToString()));

                    //// Convierte la imagen que viene en Bytes en imagen para PDF
                    //Image image = new Image(ImageDataFactory.Create(item.FotoOrden));
                    //// Tamaño de la imagen
                    //image = image.SetHeight(75).SetWidth(75);
                    //table.AddCell(image);
                }
                doc.Add(table);

                // Calculo del monto total de impuestos
                decimal totalIVA = lista.ToList().Sum(k => Convert.ToDecimal(k.TotalIVA));
                // Agrega  el monto total de impuestos
                doc.Add(new Paragraph("\n\r Total Costos " + totalIVA.ToString("C", CultureInfo.CreateSpecificCulture("cr-CR"))));

                // Calculo del monto total de impuestos
                decimal totalFinal = lista.ToList().Sum(k => Convert.ToDecimal(k.TotalFinal));
                // Agrega  el monto total de impuestos
                doc.Add(new Paragraph("\n\r Total Costos " + totalFinal.ToString("C", CultureInfo.CreateSpecificCulture("cr-CR"))));


                // Colocar número de páginas
                int numberOfPages = pdfDoc.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {

                    // Write aligned text to the specified by parameters point
                    doc.ShowTextAligned(new Paragraph(String.Format("pag {0} of {1}", i, numberOfPages)),
                            559, 826, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0);
                }


                //Close document
                doc.Close();
                // Retorna un File
                return File(ms.ToArray(), "application/pdf", "Reporte de Órdenes.pdf");

            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "¡Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }

        //Fin
    }
}