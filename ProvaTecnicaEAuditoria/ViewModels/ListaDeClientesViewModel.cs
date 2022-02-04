using ProvaTecnicaEAuditoria.Models;

namespace ProvaTecnicaEAuditoria.ViewModels
{
    public class ListaDeClientesViewModel
    {
        public ListaDeClientesViewModel(Cliente cliente)
        {
            Id = cliente.Id;
            Nome = cliente.Nome;
            Cpf = cliente.Cpf;
            DataDeNascimento = cliente.DataDeNascimento;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataDeNascimento { get; set; }
    }
}
