using LocadoraDeFilmes.Servicos.Mapeamentos;

namespace LocadoraDeFilmes.Servicos.Excel
{
    public interface IExcelServico
    {
        Task<string> GerarRelatorio(
            IEnumerable<ClienteMapeamentoDePlanilha> clientesComAtraso,
            IEnumerable<FilmeMapeamentoDePlanilha> filmesNuncaAlugados,
            IEnumerable<FilmeMapeamentoDePlanilha> filmesMaisAlugados,
            IEnumerable<FilmeMapeamentoDePlanilha> filmesMenosAlugados,
            IEnumerable<ClienteMapeamentoDePlanilha> clientesQueMaisAlugaram);
    }
}
