using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeContext _context;

		public FilmeController(FilmeContext context)
		{

			_context = context;
		}

		[HttpPost]
        public IActionResult Adicionar([FromBody] CreateFilmeDto filmeDto)
        {
            var filme = new Filme
            {
                Titulo = filmeDto.Titulo,
                Direto = filmeDto.Direto,
                Genero = filmeDto.Genero,
                Duracao = filmeDto.Duracao,

            };

            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaFilmePorId), new { id = filme.Id }, filme);
           
        }

        [HttpGet]
        public IActionResult RecuperarFilme()
        {
            return Ok(_context.Filmes ) ;
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaFilmePorId(int id )
        {

            var result = _context.Filmes.FirstOrDefault(item => item.Id == id);

            if (result != null)
			{
                var readFilme = new ReadFilmeDto
                {
                    Titulo = result.Titulo,
                    Duracao = result.Duracao,
                    Genero = result.Genero,
                    Direto = result.Direto
                    
                };

                return Ok(readFilme);
			}
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDTo filmeDto)
		{
            var result = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (result == null) return NotFound();
            result.Titulo = filmeDto.Titulo;
            result.Genero = filmeDto.Genero;
            result.Duracao =filmeDto.Duracao;
            result.Direto = filmeDto.Direto;
            _context.SaveChanges();
            return NoContent();
		}

        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
		{
            var result = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (result == null) return NotFound();
            _context.Remove(result);
            _context.SaveChanges();
            return NoContent();
		}

    }
}
