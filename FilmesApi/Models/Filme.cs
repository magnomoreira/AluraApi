using System;
using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models
{
    public class Filme
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage ="O compo titulo é obrigatorio")]
        public string Titulo { get; set; }
        [Required]
        public string Direto { get; set; }
        [StringLength(30,ErrorMessage = "Não pode ultrapassar 30 caracteres")]
        public string  Genero { get; set; }

        [Range(1, 600)]
        public int Duracao { get; set; }
    }
}
