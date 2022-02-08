
using LocadoraDeFilmes.Servicos.Mapeamentos;

namespace LocadoraDeFilmes.Servicos.Csv
{
    public interface ICsvServico
    {
        IList<FilmeMapeamentoDePlanilha> ConverterPlanilhaParaFilmes(Stream planilha, string delimitado = ";");
        bool FilmesValidos(IList<FilmeMapeamentoDePlanilha> registros);
    }
}
