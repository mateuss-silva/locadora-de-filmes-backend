using LocadoraDeFilmes.Modelos;

namespace LocadoraDeFilmes.ViewModels
{
    public class ObterClienteViewModel
    {
        public ObterClienteViewModel(Cliente cliente)
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
