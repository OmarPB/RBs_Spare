//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infraestructure.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cita
    {
        public int Id { get; set; }
        public Nullable<int> IdModelo { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidosCliente { get; set; }
        public string MotivoCita { get; set; }
        public Nullable<System.DateTime> FechaCita { get; set; }
        public Nullable<System.TimeSpan> HoraCita { get; set; }
        public Nullable<bool> Condicion { get; set; }
    
        public virtual ModeloMoto ModeloMoto { get; set; }
    }
}