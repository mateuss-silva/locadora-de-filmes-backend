using ProvaTecnicaEAuditoria.Modelos;
using System.ComponentModel.DataAnnotations;

namespace ProvaTecnicaEAuditoria.ViewModels
{
    public class AtualizarLocacaoViewModel
    {
        public AtualizarLocacaoViewModel() { }
        public AtualizarLocacaoViewModel(Locacao locacao)
        {
            ClienteId = locacao.ClienteId;
            FilmeId = locacao.FilmeId;
            DataDeDevolucao = locacao.DataDeDevolucao;
        }

        [Required(ErrorMessage = "O Id do Cliente é obrigatório")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "O Id do Filme é obrigatório")]
        public int FilmeId { get; set; }

        public DateTime? DataDeDevolucao { get; set; }
    }
}
