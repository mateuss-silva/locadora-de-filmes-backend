using ProvaTecnicaEAuditoria.Modelos;
using System.ComponentModel.DataAnnotations;

namespace ProvaTecnicaEAuditoria.ViewModels
{
    public class AtualizarClienteViewModel
    {
        public AtualizarClienteViewModel() { }

        public AtualizarClienteViewModel(Cliente cliente)
        {
            Nome = cliente.Nome;
            Cpf = cliente.Cpf;
            DataDeNascimento = cliente.DataDeNascimento;
        }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(200, ErrorMessage = "O nome de conter entre 1 e 200 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O Cpf é obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O Cpf deve ter conter 11 caracteres")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "A Data de Nascimento é obrigatória")]
        public DateTime DataDeNascimento { get; set; }
    }
}
