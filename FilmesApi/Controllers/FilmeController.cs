using AutoMapper;
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
        private IMapper _maper;

		public FilmeController(FilmeContext context, IMapper mapper)
		{

			_context = context;
            _maper = mapper;
		}

		[HttpPost]
        public IActionResult Adicionar([FromBody] CreateFilmeDto filmeDto)
        {
            var filme = _maper.Map<Filme>(filmeDto);

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
                var readFilme = _maper.Map<ReadFilmeDto>(result);
                return Ok(readFilme);
			}
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDTo filmeDto)
		{
            var result = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (result == null) return NotFound();
            _maper.Map(filmeDto, result);
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
