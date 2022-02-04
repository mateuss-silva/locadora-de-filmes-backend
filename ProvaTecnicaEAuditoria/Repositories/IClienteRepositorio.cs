using ProvaTecnicaEAuditoria.Models;

namespace ProvaTecnicaEAuditoria.Repositories
{
    public interface IClienteRepositorio: IDisposable
    {
        IList<Cliente> ObterIntervalo(int pular, int pegar);
        Cliente ObterPorId(int id);
        bool CpfExistente(string cpf);
        bool ClienteExistente(int id);
        void Inserir(Cliente cliente);
        void Atualizar(Cliente cliente);
        void Deletar(Cliente cliente);
    }
}
