using LocadoraDeFilmes.Modelos;

namespace LocadoraDeFilmes.ViewModels
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
            NomeDoFilme = locacao.Filme.Titulo;
            NomeDoCliente = locacao.Cliente.Nome;
            Cpf = locacao.Cliente.Cpf;
        }

        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int FilmeId { get; set; }
        public string NomeDoFilme { get; set; }
        public string NomeDoCliente { get; set; }
        public string Cpf { get; set; }
        public DateTime DataDeLocacao { get; set; }
        public DateTime? DataDeDevolucao { get; set; }
    }
}
