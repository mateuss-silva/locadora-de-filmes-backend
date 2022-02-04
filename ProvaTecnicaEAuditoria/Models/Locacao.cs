
namespace ProvaTecnicaEAuditoria.Models
{
    public class Locacao
    {
        public Locacao() { }

        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int FilmeId { get; set; }
        public DateTime DataDeLocacao { get; set; }
        public DateTime DataDeDevolucao { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Filme Filme { get; set; }

    }
}
