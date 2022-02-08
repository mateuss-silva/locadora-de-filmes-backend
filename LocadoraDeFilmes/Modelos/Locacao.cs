
using LocadoraDeFilmes.ViewModels;

namespace LocadoraDeFilmes.Modelos
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

            if (locacao.DataDeDevolucao is not null) DataDeDevolucao = locacao.DataDeDevolucao;
        }

        public bool DevolucaoAtrasada()
        {
            if (DataDeDevolucao is null)
            {
                var prazoDeEntrega = Filme.Lancamento ? 2 : 3;

                var diasAlugados = (DateTime.Now - DataDeLocacao).TotalDays;

                return diasAlugados > prazoDeEntrega;
            }

            return false;
        }

        public bool DevolucaoDepoisDoPrazo()
        {
            if (DataDeDevolucao is not null)
            {
                var prazoDeEntrega = Filme.Lancamento ? 2 : 3;

                var diasAlugados = (DataDeDevolucao.Value - DataDeLocacao).TotalDays;

                return diasAlugados > prazoDeEntrega;
            }

            return false;
        }
    }
}
