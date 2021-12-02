using ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{
    public class RespaldosController : Controller
    {
        // GET: Respaldos
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult guardarRespaldo()
        {
            IServiceRespaldos serviceRespaldos = new ServiceRespaldos();
            serviceRespaldos.guardarRespaldo();

            return View("respaldoExitoso");
        }

        public ActionResult restaurarRespaldo(string ruta)
        {
            ruta = ruta.Substring(12);

            if (ruta.Contains(".bak"))
            {
                ruta = @"C:\RespaldosRBsSpare\" + ruta;
                IServiceRespaldos serviceRespaldos = new ServiceRespaldos();
                serviceRespaldos.restaurarRespaldo(ruta);
            }

            return View("restauracionExitosa");
        }

        //Fin
    }
}