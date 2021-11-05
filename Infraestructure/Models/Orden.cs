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
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(OrdenMetadata))]
    public partial class Orden
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orden()
        {
            this.Detalle_Orden = new HashSet<Detalle_Orden>();
        }
    
        public int Id { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidosCliente { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public Nullable<decimal> Subtotal { get; set; }
        public Nullable<decimal> TotalIVA { get; set; }
        public Nullable<decimal> TotalFinal { get; set; }
        public Nullable<int> IdCondicionOrden { get; set; }
    
        public virtual CondicionOrden CondicionOrden { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Detalle_Orden> Detalle_Orden { get; set; }
    }
}
