using Microsoft.EntityFrameworkCore;
using LocadoraDeFilmes.Dados;
using LocadoraDeFilmes.Modelos;
using LocadoraDeFilmes.Repositorios.Interfaces;

namespace LocadoraDeFilmes.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private EAuditoriaContexto _auditoriaDataContext;

        public ClienteRepositorio(EAuditoriaContexto eAuditoriaDataContext)
        {
            _auditoriaDataContext = eAuditoriaDataContext;
        }

        public int ObterQuantidadeDeClientes(string busca)
        {
            busca = busca.ToLower();

            return _auditoriaDataContext.Clientes
            .AsNoTracking()
            .Where(x => x.Cpf.Contains(busca) || x.Nome.ToLower().Contains(busca))
            .Count();
        }

        public IList<Cliente> ObterIntervalo(string busca, int pular, int pegar)
        {
            busca = busca.ToLower();

            return _auditoriaDataContext.Clientes
            .AsNoTracking()
            .Where(x => x.Cpf.Contains(busca) || x.Nome.ToLower().Contains(busca))
            .Skip(pular)
            .Take(pegar)
            .ToList();
        }

        public IList<Cliente> ObterClientesQueMaisAlugaram(int pular, int pegar)
        {
            return _auditoriaDataContext.Clientes
             .AsNoTracking()
             .Include(e => e.Locacoes)
             .OrderByDescending(e => e.Locacoes.Count)
             .Skip(pular)
             .Take(pegar)
             .ToList();
        }

        public IList<Cliente> ObterClientesEmAtrasoDeDevolucao()
        {
            var clientes = _auditoriaDataContext.Clientes
                            .AsNoTracking()
                            .Include(e => e.Locacoes)
                            .ThenInclude(e => e.Filme)
                            .AsEnumerable();

            var clientesComAtrasoDeFilme = clientes.Where(x => x.Locacoes.Any(e => e.DevolucaoAtrasada())).ToList();

            return clientesComAtrasoDeFilme;
        }

        public Cliente ObterPorId(int id)
        {
            return _auditoriaDataContext.Clientes.Find(id);
        }


        public void Inserir(Cliente cliente)
        {
            _auditoriaDataContext.Clientes.Add(cliente);
            _auditoriaDataContext.SaveChanges();
        }

        public void Atualizar(Cliente cliente)
        {
            _auditoriaDataContext.Clientes.Update(cliente);
            _auditoriaDataContext.SaveChanges();

        }

        public void Deletar(Cliente cliente)
        {
            _auditoriaDataContext.Clientes.Remove(cliente);
            _auditoriaDataContext.SaveChanges();

        }

        public bool CpfExistente(string cpf)
        {
            return _auditoriaDataContext.Clientes.Any(x => x.Cpf.Equals(cpf));
        }

        public void Dispose()
        {
            _auditoriaDataContext.Dispose();
        }
    }
}
