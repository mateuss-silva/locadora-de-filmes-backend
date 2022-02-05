using ProvaTecnicaEAuditoria.Servicos.Csv.Mapeamentos;

namespace ProvaTecnicaEAuditoria.Modelos
{
    public class Filme
    {
        public Filme() { }
        public Filme(FilmeMapeamentoCsv filme) 
        { 
            Titulo = filme.Titulo;
            ClassificacaoIndicativa = filme.ClassificacaoIndicativa;
            Lancamento = filme.Lancamento;
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public int ClassificacaoIndicativa { get; set; }
        public bool Lancamento { get; set; }
        public virtual List<Locacao> Locacoes { get; set; }

        public bool NuncaAlugado() => Locacoes.Count == 0;
    }
}
