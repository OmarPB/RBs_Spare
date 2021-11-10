using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.ViewModel
{
    public class ViewModelParametro
    {
        [Required(ErrorMessage = "El dato {0} es requerido")]
        [DataType(DataType.Date)]
        [Display(Name = "Seleccione el Mes")]
        //Se necesita el mes y el año
        public DateTime Fecha { get; set; }
    }
}