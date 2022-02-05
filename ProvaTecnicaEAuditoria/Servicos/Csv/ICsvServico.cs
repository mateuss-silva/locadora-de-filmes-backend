
using ProvaTecnicaEAuditoria.Servicos.Csv.Mapeamentos;

namespace ProvaTecnicaEAuditoria.Servicos.Csv
{
    public interface ICsvServico
    {
        IList<FilmeMapeamentoCsv> ConverterPlanilhaParaFilmes(Stream planilha, string delimitado = ";");
        bool FilmesValidos(IList<FilmeMapeamentoCsv> registros);
    }
}
