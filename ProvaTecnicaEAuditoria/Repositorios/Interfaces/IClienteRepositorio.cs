
using ProvaTecnicaEAuditoria.Modelos;

namespace ProvaTecnicaEAuditoria.Repositorios.Interfaces
{
    public interface IClienteRepositorio: IDisposable
    {
        int ObterQuantidadeDeClientes(string busca);
        IList<Cliente> ObterIntervalo(string busca, int pular, int pegar);
        IList<Cliente> ObterClientesEmAtrasoDeDevolucao();
        IList<Cliente> ObterClientesQueMaisAlugaram(int pular, int pegar);
        Cliente ObterPorId(int id);
        bool CpfExistente(string cpf);
        void Inserir(Cliente cliente);
        void Atualizar(Cliente cliente);
        void Deletar(Cliente cliente);
    }
}
