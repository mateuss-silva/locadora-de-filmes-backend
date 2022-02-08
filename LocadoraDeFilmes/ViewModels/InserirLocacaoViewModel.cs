using LocadoraDeFilmes.Modelos;
using System.ComponentModel.DataAnnotations;

namespace LocadoraDeFilmes.ViewModels
{
    public class InserirLocacaoViewModel
    {
        public InserirLocacaoViewModel() { }

        [Required(ErrorMessage = "O Id do Cliente é obrigatório")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "O Id do Filme é obrigatório")]
        public int FilmeId { get; set; }
    }
}
