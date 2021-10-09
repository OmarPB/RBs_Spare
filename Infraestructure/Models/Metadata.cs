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
        [Range(0, int.MaxValue, ErrorMessage = "Favor ingrese únicamente números")]
        public Nullable<int> Telefono { get; set; }

        public Nullable<bool> Estado { get; set; }

    }

    internal partial class RolMetadata
    {
        public int Id { get; set; }

        [Display(Name = "Rol")]
        public string Descripcion { get; set; }
    }
}
