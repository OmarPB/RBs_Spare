using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    //Se crea un internal partial class para cada clase con su nombre "nombreClaseMetadata",
    //y allí se hacen los DataAnotations de cada clase
    internal partial class EmpleadoMetadata
    {
        [Display(Name = "Cédula")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "Favor ingrese únicamente números")]
        public int Id { get; set; }

        [Display(Name = "Rol")]
        public Nullable<int> IdRol { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Por favor digite una {0}")]
        [RegularExpression(@"^(?=.*\d)(?=.*[\u0021-\u002b\u003c-\u0040])(?=.*[A-Z])(?=.*[a-z])\S{9,100}$", ErrorMessage = "La contraseña debe contener al menos 9 caracteres, una mayúscula, un número y un caracter especial")]
        [DataType(DataType.Password)]
        public string Contrasenia { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Apellidos { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [EmailAddress(ErrorMessage = "Por favor revise el formato de su {0}")]
        public string Email { get; set; }

        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(10000000, 99999999, ErrorMessage = "El formato debe ser 88888888")]
        public Nullable<int> Telefono { get; set; }

        public Nullable<bool> Estado { get; set; }

    }

    internal partial class ProductoMetadata
    {
        public int Id { get; set; }

        [Display(Name = "Tipo")]
        public Nullable<int> IdTipoProducto { get; set; }

        [Display(Name = "Marca")]
        public Nullable<int> IdMarca { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Descripcion { get; set; }

        public byte[] Imagen { get; set; }

        [Display(Name = "Precio Unidad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public Nullable<decimal> PrecioUnidad { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayFormat(DataFormatString = "{0:0.0%}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> IVA { get; set; }

        public Nullable<bool> Estado { get; set; }
    }

    internal partial class RolMetadata
    {
        public int Id { get; set; }

        [Display(Name = "Rol")]
        public string Descripcion { get; set; }
    }

    internal partial class TipoProductoMetadata
    {
        public int Id { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Descripcion { get; set; }
        public Nullable<bool> Estado { get; set; }
    }

    internal partial class MarcaProductoMetadata
    {
        public int Id { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Descripcion { get; set; }
        public Nullable<bool> Estado { get; set; }
    }

    internal partial class OrdenMetadata
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Por favar ingrese su {0}")]
        public string NombreCliente { get; set; }

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "Por favar ingrese sus {0}")]
        public string ApellidosCliente { get; set; }

        [Display(Name = "Fecha de Creación")]
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public Nullable<decimal> Subtotal { get; set; }
        public Nullable<decimal> TotalIVA { get; set; }
        public Nullable<decimal> TotalFinal { get; set; }
        public Nullable<int> IdCondicionOrden { get; set; }
    }

    //Fin
}
