
using LocadoraDeFilmes.Modelos;

namespace LocadoraDeFilmes.Repositorios.Interfaces
{
    public interface ILocacaoRepositorio: IDisposable
    {
        int ObterQuantidadeDeLocacoes(string busca);
        IList<Locacao> ObterIntervalo(string busca,int pular, int pegar);
        Locacao ObterPorId(int id);
        void Inserir(Locacao locacao);
        void Atualizar(Locacao locacao);
        void Deletar(Locacao locacao);
        bool LocacaoValida(int clienteId,int filmeId);
    }
}
