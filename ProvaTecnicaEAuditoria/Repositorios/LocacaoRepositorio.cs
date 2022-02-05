using Microsoft.EntityFrameworkCore;
using ProvaTecnicaEAuditoria.Dados;
using ProvaTecnicaEAuditoria.Modelos;

namespace ProvaTecnicaEAuditoria.Repositorios
{
    public class LocacaoRepositorio : ILocacaoRepositorio
    {

        private EAuditoriaContexto _auditoriaDataContext;

        public LocacaoRepositorio(EAuditoriaContexto eAuditoriaDataContext)
        {
            _auditoriaDataContext = eAuditoriaDataContext;
        }

        public int ObterQuantidadeDeLocacoes(string busca)
        {
            busca = busca.ToLower();

            return _auditoriaDataContext.Locacoes
            .AsNoTracking()
            .Include(x => x.Filme)
            .Include(x => x.Cliente)
            .Where(x => x.Cliente.Cpf.Contains(busca) || x.Filme.Titulo.ToLower().Contains(busca))
            .Count();
        }

        public IList<Locacao> ObterIntervalo(string busca, int pular, int pegar)
        {
            busca = busca.ToLower();

            return _auditoriaDataContext.Locacoes
            .AsNoTracking()
            .Include(x => x.Filme)
            .Include(x => x.Cliente)
            .Where(x => x.Cliente.Cpf.Contains(busca) || x.Filme.Titulo.ToLower().Contains(busca))
            .Skip(pular).Take(pegar).ToList();
        }

        public Locacao ObterPorId(int id)
        {
            return _auditoriaDataContext.Locacoes
            .Include(x => x.Filme)
            .Include(x => x.Cliente)
            .FirstOrDefault(x => x.Id.Equals(id));
        }

        public void Inserir(Locacao locacao)
        {
            _auditoriaDataContext.Locacoes.Add(locacao);
            _auditoriaDataContext.SaveChanges();
        }

        public void Atualizar(Locacao locacao)
        {
            _auditoriaDataContext.Locacoes.Update(locacao);
            _auditoriaDataContext.SaveChanges();
        }

        public void Deletar(Locacao locacao)
        {
            _auditoriaDataContext.Locacoes.Remove(locacao);
            _auditoriaDataContext.SaveChanges();
        }

        public bool LocacaoValida(int clienteId, int filmeId)
        {
            var clienteExistente = _auditoriaDataContext.Clientes.Any(e => e.Id.Equals(clienteId));
            var filmeExistente = _auditoriaDataContext.Filmes.Any(e => e.Id.Equals(filmeId));

            return clienteExistente && filmeExistente;
        }

        public void Dispose()
        {
            _auditoriaDataContext.Dispose();
        }
    }
}
