using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAutores.DTOs;

namespace WebApiAutores.Entidades
{
    public class AutorDTOConLibros:AutorDTO
    {
        public List<LibroDTO> Libros { get; set; }
    }
}
