using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiAutores.Validaciones;

namespace WebApiAutores.DTOs
{
    public class AutorCreacionDTO
    {
        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(maximumLength: 120, ErrorMessage = "EL Campo {0} no puede tener mas de {1} caracteres")]
        [PrimerLetraMayuscula]
        public string Nombre { get; set; }
    }
}
