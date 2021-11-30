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
        [RegularExpression(@"^(?=.*[A-z])(?=.*[A-Z])(?=.*\D)[a-zA-Z\d\w\W]{9,}$", ErrorMessage = "La contraseña debe tener al menos 9 caracteres con mayúsculas, minúsculas, números y caracteres especiales")]        //[DataType(DataType.Password)]
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
        public Nullable<decimal> PrecioUnidad { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
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


    internal partial class CitaMetadata
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name ="Modelo de la moto")]
        public Nullable<int> IdModelo { get; set; }
        
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name ="Nombre")]
        public string NombreCliente { get; set; }
        
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name ="Apellidos")]
        public string ApellidosCliente { get; set; }
        
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name ="Motivo de la cita")]
        public string MotivoCita { get; set; }
        
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name ="Fecha deseada")]
        public Nullable<System.DateTime> FechaCita { get; set; }
        
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Hora deseada")]
        public Nullable<System.TimeSpan> HoraCita { get; set; }
        
        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(10000000, 99999999, ErrorMessage = "El formato debe ser 88888888")]
        public Nullable<int> TelefonoCliente { get; set; }
        public Nullable<bool> Condicion { get; set; }

        public virtual ModeloMoto ModeloMoto { get; set; }
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

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public Nullable<decimal> Subtotal { get; set; }

        [Display(Name = "Total Impuesto")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public Nullable<decimal> TotalIVA { get; set; }

        [Display(Name = "Total Final")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public Nullable<decimal> TotalFinal { get; set; }
        [Display(Name = "Condición")]
        public Nullable<int> IdCondicionOrden { get; set; }
    }

    //Fin
}
