using ProvaTecnicaEAuditoria.Models;
using System.ComponentModel.DataAnnotations;

namespace ProvaTecnicaEAuditoria.ViewModels
{
    public class EditarLocacaoViewModel
    {
        public EditarLocacaoViewModel() { }
        public EditarLocacaoViewModel(Locacao locacao)
        {
            ClienteId = locacao.ClienteId;
            FilmeId = locacao.FilmeId;
            DataDeLocacao = locacao.DataDeLocacao;
            DataDeDevolucao = locacao.DataDeDevolucao;
        }

        [Required(ErrorMessage = "O Id do Cliente é obrigatório")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "O Id do Filme é obrigatório")]
        public int FilmeId { get; set; }

        [Required(ErrorMessage = "A Data de locação é obrigatória")]
        public DateTime DataDeLocacao { get; set; }

        [Required(ErrorMessage = "A Data de Devolução é obrigatória")]
        public DateTime DataDeDevolucao { get; set; }
    }
}
