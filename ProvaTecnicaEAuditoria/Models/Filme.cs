
namespace ProvaTecnicaEAuditoria.Models
{
    public class Filme
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int ClassificacaoIndicativa { get; set; }
        public short Lancamento { get; set; }
        public virtual IEnumerable<Locacao> Locacoes { get; set; }

    }
}
