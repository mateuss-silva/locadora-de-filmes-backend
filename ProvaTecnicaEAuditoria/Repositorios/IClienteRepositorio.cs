
using ProvaTecnicaEAuditoria.Modelos;

namespace ProvaTecnicaEAuditoria.Repositorios
{
    public interface IClienteRepositorio: IDisposable
    {
        IList<Cliente> ObterIntervalo(int pular, int pegar);
        Cliente ObterPorId(int id);
        bool CpfExistente(string cpf);
        void Inserir(Cliente cliente);
        void Atualizar(Cliente cliente);
        void Deletar(Cliente cliente);
    }
}
