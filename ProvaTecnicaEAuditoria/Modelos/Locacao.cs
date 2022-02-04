
using ProvaTecnicaEAuditoria.ViewModels;

namespace ProvaTecnicaEAuditoria.Modelos
{
    public class Locacao
    {
        public Locacao() { }
        public Locacao(InserirLocacaoViewModel locacao) 
        {
            ClienteId = locacao.ClienteId;
            FilmeId = locacao.FilmeId;
            DataDeLocacao = DateTime.Now;
        }

        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int FilmeId { get; set; }
        public DateTime DataDeLocacao { get; set; }
        public DateTime? DataDeDevolucao { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Filme Filme { get; set; }

        public void Atualizar(AtualizarLocacaoViewModel locacao)
        {
            ClienteId = locacao.ClienteId;
            FilmeId = locacao.FilmeId;
            DataDeDevolucao = locacao.DataDeDevolucao;
        }
    }
}
