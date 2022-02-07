using ProvaTecnicaEAuditoria.Modelos;

namespace ProvaTecnicaEAuditoria.Servicos.Mapeamentos
{
    public class ClienteMapeamentoDePlanilha
    {
        public ClienteMapeamentoDePlanilha() { }
        public ClienteMapeamentoDePlanilha(Cliente cliente)
        {
            Id = cliente.Id;
            Nome = cliente.Nome;
            Cpf = cliente.Cpf;
            DataDeNascimento = cliente.DataDeNascimento.ToString("dd/MM/yyyy");
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public String DataDeNascimento { get; set; }
    }
}
