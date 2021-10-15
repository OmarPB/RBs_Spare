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
        [RegularExpression(@"^(?=.*\d)(?=.*[\u0021-\u002b\u003c-\u0040])(?=.*[A-Z])(?=.*[a-z])\S{9-50}$", ErrorMessage = "La contraseña debe contener al menos 9 caracteres, una mayúscula, un número y un caracter especial")]
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

    //Fin
}
