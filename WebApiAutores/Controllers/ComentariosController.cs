using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/libros/{libroId:int}/comentarios")]
    public class ComentariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ComentariosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<ComentarioDTO>>> Get(int libroId)
        {
            var existeLibro = await context.Libros.AnyAsync(libroDB => libroDB.Id == libroId);

            if (!existeLibro)
            {
                return NotFound();
            }

            var comentarios = await context.Comentarios
                .Where(comentarioDB => comentarioDB.LibroId == libroId).ToListAsync();

            return mapper.Map<List<ComentarioDTO>>(comentarios);
        }
        [HttpGet("{id:int}",Name ="ObtenerComentario")]
        public async Task<ActionResult<ComentarioDTO>> GetPorId(int id)
        {
            var comentario = await context.Comentarios.FirstOrDefaultAsync(c => c.Id == id);
            
            if (comentario==null)
            {
                return NotFound();
            }
            return mapper.Map<ComentarioDTO>(comentario);            
        }
        [HttpPost]
        public async Task<ActionResult> Post(int libroId, ComentarioCreactionDTO comentarioCreactionDTO)
        {
            var existeLibro = await context.Libros.AnyAsync(libroDB => libroDB.Id == libroId);
            if (!existeLibro)
            {
                return NotFound();
            }
            var comentario = mapper.Map<Comentario>(comentarioCreactionDTO);
            comentario.LibroId = libroId;
            context.Add(comentario);
            await context.SaveChangesAsync();
            
            var comentarioDTO = mapper.Map<ComentarioDTO>(comentario);
            
            return CreatedAtRoute("ObtenerComentario", new { id = comentario.Id,LibroID=libroId }, comentarioDTO);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult>Put (int libroId,int id, ComentarioCreactionDTO comentarioCreactionDTO)
        {
            var existeLibro = await context.Libros.AnyAsync(libroDB => libroDB.Id == libroId);
            if (!existeLibro)
            {
                return NotFound();
            }
            var existeComentario = await context.Comentarios.AnyAsync(c => c.Id == id);

            if (!existeComentario)
            {
                return NotFound();

            }

            var comentario = mapper.Map<Comentario>(comentarioCreactionDTO);
            comentario.Id = id;
            comentario.LibroId = libroId;
            context.Update(comentario);
            await context.SaveChangesAsync();
            return NoContent();

        }
    }
}
