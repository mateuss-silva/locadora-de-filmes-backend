using Microsoft.EntityFrameworkCore;
using ProvaTecnicaEAuditoria.Dados;
using ProvaTecnicaEAuditoria.Modelos;

namespace ProvaTecnicaEAuditoria.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private EAuditoriaContexto _auditoriaDataContext;

        public ClienteRepositorio(EAuditoriaContexto eAuditoriaDataContext)
        {
            _auditoriaDataContext = eAuditoriaDataContext;
        }

        public IList<Cliente> ObterIntervalo(int pular, int pegar)
        {
            return _auditoriaDataContext.Clientes.AsNoTracking().Skip(pular).Take(pegar).ToList();
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
