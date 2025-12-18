using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace ML
{
    public class Usuario
    {
        public string IdUsuario { get; set; }
        public string NombreCompleto { get; set; }  
        public string Hobby { get; set; }
        public DateTime? FechaNacimiento { get; set; }

        [DisplayName("Correo Electronico")]
        [Required(ErrorMessage = "Formato no válido.")]
        [RegularExpression("^[^@]+@[^@]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Por favor, llene el campo")]
        public string Email { get; set; }

        public List<object> Usuarios { get; set; }
    }
}
