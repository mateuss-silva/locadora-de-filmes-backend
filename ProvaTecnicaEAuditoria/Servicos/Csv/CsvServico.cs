using CsvHelper;
using CsvHelper.Configuration;
using ProvaTecnicaEAuditoria.Servicos.Csv.Mapeamentos;
using System.Globalization;

namespace ProvaTecnicaEAuditoria.Servicos.Csv
{
    public class CsvServico: ICsvServico
    {
        public IList<FilmeMapeamentoCsv> ConverterPlanilhaParaFilmes(Stream planilha, string delimitado = ";")
        {

            using TextReader conteudoCsv = new StreamReader(planilha);

            var configuracao = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                MissingFieldFound = null,
                Delimiter = delimitado,
            };

            using var leitorCsv = new CsvReader(conteudoCsv, configuracao);

            var registros = leitorCsv.GetRecords<FilmeMapeamentoCsv>().ToList();

            return registros;

        }

        public bool FilmesValidos(IList<FilmeMapeamentoCsv> registros)
        {
            var contemIdNulo = registros.Any(x => x.Id is null);

            if (contemIdNulo) return false;

            var registrosDistintos = registros.DistinctBy(x => x.Id).ToList();

            var registrosValidos = registrosDistintos.Count == registros.Count;

            return registrosValidos;
        }
    }
}
