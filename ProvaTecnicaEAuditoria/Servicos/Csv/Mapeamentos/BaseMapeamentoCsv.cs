using CsvHelper.Configuration.Attributes;

namespace ProvaTecnicaEAuditoria.Servicos.Csv.Mapeamentos
{
    public class BaseMapeamentoCsv
    {
        [Name("Id")]
        public int? Id { get; set; }
    }
}
