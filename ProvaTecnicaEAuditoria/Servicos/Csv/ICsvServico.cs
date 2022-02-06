
using ProvaTecnicaEAuditoria.Servicos.Mapeamentos;

namespace ProvaTecnicaEAuditoria.Servicos.Csv
{
    public interface ICsvServico
    {
        IList<FilmeMapeamentoDePlanilha> ConverterPlanilhaParaFilmes(Stream planilha, string delimitado = ";");
        bool FilmesValidos(IList<FilmeMapeamentoDePlanilha> registros);
    }
}
