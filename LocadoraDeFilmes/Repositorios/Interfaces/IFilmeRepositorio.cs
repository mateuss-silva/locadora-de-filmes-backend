using ProvaTecnicaEAuditoria.Modelos;

namespace ProvaTecnicaEAuditoria.Repositorios.Interfaces
{
    public interface IFilmeRepositorio: IDisposable
    {
        int ObterQuantidadeDeFilmes(string busca);
        IList<Filme> ObterIntervalo(string busca, int pular, int pegar);
        IList<Filme> ObterMaisAlugadosPorIntervalo(int pegar, DateTime dataInicial);
        IList<Filme> ObterMenosAlugadosPorIntervalo(int pegar, DateTime dataInicial);
        IList<Filme> ObterFilmesNuncaAlugados();
       void InserirFilmes(IList<Filme> filmes);

    }
}
