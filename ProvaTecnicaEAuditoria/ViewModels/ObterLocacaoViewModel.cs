using ProvaTecnicaEAuditoria.Models;

namespace ProvaTecnicaEAuditoria.ViewModels
{
    public class ObterLocacaoViewModel
    {
        public ObterLocacaoViewModel(Locacao locacao)
        {
            Id = locacao.Id;
            ClienteId = locacao.ClienteId;
            FilmeId = locacao.FilmeId;
            DataDeLocacao = locacao.DataDeLocacao;
            DataDeDevolucao = locacao.DataDeDevolucao;
        }

        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int FilmeId { get; set; }
        public DateTime DataDeLocacao { get; set; }
        public DateTime DataDeDevolucao { get; set; }
    }
}
