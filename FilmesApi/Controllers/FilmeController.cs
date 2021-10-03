using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FilmesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private static List<Filme> filmes = new List<Filme>();
        private static int id = 1;

        [HttpPost]
        public IActionResult Adicionar([FromBody] Filme filme)
        {
            filme.Id = id++;
            filmes.Add(filme);
            return CreatedAtAction(nameof(RecuperaFilmePorId), new { id = filme.Id }, filme);
           
        }

        [HttpGet]
        public IActionResult RecuperarFilme()
        {
            return Ok( filmes) ;
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaFilmePorId(int id )
        {
           var result =  filmes.FirstOrDefault(filme => filme.Id == id);
            return Ok(result);
        }

    }
}
