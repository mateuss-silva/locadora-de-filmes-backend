using CsvHelper.Configuration.Attributes;

namespace ProvaTecnicaEAuditoria.Servicos.Csv.Mapeamentos
{
    public class FilmeMapeamentoCsv : BaseMapeamentoCsv
    {
        [Name("Titulo")]
        public string Titulo { get; set; }
        [Name("ClassificacaoIndicativa")]
        public int ClassificacaoIndicativa { get; set; }
        [Name("Lancamento")]
        public bool Lancamento { get; set; }
    }
}
