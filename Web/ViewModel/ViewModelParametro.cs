using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.ViewModel
{
    public class ViewModelParametro
    {
        [Required(ErrorMessage = "El dato es requerido")]
        [Display(Name = "Seleccione el Mes")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        //Se necesita el mes y el año
        public DateTime Fecha { get; set; }
    }
}