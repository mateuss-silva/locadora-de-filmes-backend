using CsvHelper.Configuration.Attributes;
using ProvaTecnicaEAuditoria.Modelos;

namespace ProvaTecnicaEAuditoria.Servicos.Mapeamentos
{
    public class FilmeMapeamentoDePlanilha
    {
        public FilmeMapeamentoDePlanilha() { }

        public FilmeMapeamentoDePlanilha(Filme filme) 
        {
            Id = filme.Id;
            Titulo = filme.Titulo;
            ClassificacaoIndicativa = filme.ClassificacaoIndicativa;
            Lancamento = filme.Lancamento;
        }

        [Name("Id")]
        public int? Id { get; set; }
        [Name("Titulo")]
        public string Titulo { get; set; }
        [Name("ClassificacaoIndicativa")]
        public int ClassificacaoIndicativa { get; set; }
        [Name("Lancamento")]
        public bool Lancamento { get; set; }
    }
}
