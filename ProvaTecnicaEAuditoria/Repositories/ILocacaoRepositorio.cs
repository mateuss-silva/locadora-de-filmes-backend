using ProvaTecnicaEAuditoria.Models;

namespace ProvaTecnicaEAuditoria.Repositories
{
    public interface ILocacaoRepositorio: IDisposable
    {
        IList<Locacao> ObterIntervalo(int pular, int pegar);
        Locacao ObterPorId(int id);
        void Inserir(Locacao locacao);
        void Atualizar(Locacao locacao);
        void Deletar(Locacao locacao);
        bool LocacaoValida(int clienteId,int filmeId);
    }
}
