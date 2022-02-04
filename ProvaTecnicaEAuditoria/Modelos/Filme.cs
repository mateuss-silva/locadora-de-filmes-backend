
namespace ProvaTecnicaEAuditoria.Modelos
{
    public class Filme
    {
        public Filme() { }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public int ClassificacaoIndicativa { get; set; }
        public bool Lancamento { get; set; }
        public virtual List<Locacao> Locacoes { get; set; }

    }
}
