
using ProvaTecnicaEAuditoria.ViewModels;

namespace ProvaTecnicaEAuditoria.Models
{
    public class Locacao
    {
        public Locacao() { }
        public Locacao(EditarLocacaoViewModel locacao) 
        {
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
        public virtual Cliente Cliente { get; set; }
        public virtual Filme Filme { get; set; }

        public void Atualizar(EditarLocacaoViewModel locacao)
        {
            ClienteId = locacao.ClienteId;
            FilmeId = locacao.FilmeId;
            DataDeLocacao = locacao.DataDeLocacao;
            DataDeDevolucao = locacao.DataDeDevolucao;
        }
    }
}
