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
    
    public partial class BitacoraEmpleados
    {
        public int Id { get; set; }
        public Nullable<int> IdEmpleadoEjecutor { get; set; }
        public string NombreEmpleadoEjecutor { get; set; }
        public string DatoAnterior { get; set; }
        public string DatosNuevo { get; set; }
        public Nullable<System.DateTime> FechaCambios { get; set; }
        public string Accion { get; set; }
    
        public virtual Empleado Empleado { get; set; }
    }
}
