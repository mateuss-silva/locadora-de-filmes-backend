using ProvaTecnicaEAuditoria.Servicos.Mapeamentos;

namespace ProvaTecnicaEAuditoria.Servicos.Excel
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
