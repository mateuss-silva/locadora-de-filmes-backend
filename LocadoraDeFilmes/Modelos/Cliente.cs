
using ProvaTecnicaEAuditoria.ViewModels;

namespace ProvaTecnicaEAuditoria.Modelos
{
    public class Cliente
    {
        public Cliente() { }

        public Cliente(AtualizarClienteViewModel clienteViewModel)
        {
            Nome = clienteViewModel.Nome;
            Cpf = clienteViewModel.Cpf;
            DataDeNascimento = clienteViewModel.DataDeNascimento;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public virtual List<Locacao> Locacoes { get; set; }

        public void Atualizar(AtualizarClienteViewModel clienteViewModel)
        {
            Nome = clienteViewModel.Nome;
            Cpf = clienteViewModel.Cpf;
            DataDeNascimento = clienteViewModel.DataDeNascimento;
        }
    }
}
