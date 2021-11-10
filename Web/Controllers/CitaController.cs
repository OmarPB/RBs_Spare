using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class CitaController : Controller
    {
        private static String Action;
        // GET: Cita
        public ActionResult Index()
        {
            IServiceModeloMoto serviceModelo = new ServiceModeloMoto();
            ViewBag.listaModelos = serviceModelo.GetModeloMoto();
            return View();
        }


        public ActionResult Save(Cita cita)
        {
            
            string errores = "";
            try
            {
                // Es valido
                if (ModelState.IsValid)
                {
                    ServiceCita _ServiceCita = new ServiceCita();
                    if (_ServiceCita.GetCita(cita) == null)
                    {
                        _ServiceCita.Save(cita);
                    }
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);

                    TempData["Message"] = "Error al procesar los datos! " + errores;
                    TempData.Keep();

                    return View("Index", cita);
                }

                Action = "S";

                // redirigir
                return RedirectToAction("Index");
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