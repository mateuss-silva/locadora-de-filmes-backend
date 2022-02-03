
namespace ProvaTecnicaEAuditoria.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public IEnumerable<Locacao> Locacoes { get; set; }
    }
}
