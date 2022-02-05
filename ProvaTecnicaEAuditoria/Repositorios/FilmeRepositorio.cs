using Microsoft.EntityFrameworkCore;
using ProvaTecnicaEAuditoria.Dados;
using ProvaTecnicaEAuditoria.Modelos;

namespace ProvaTecnicaEAuditoria.Repositorios
{
    public class FilmeRepositorio : IFilmeRepositorio
    {

        private EAuditoriaContexto _auditoriaDataContext;

        public FilmeRepositorio(EAuditoriaContexto eAuditoriaDataContext)
        {
            _auditoriaDataContext = eAuditoriaDataContext;
        }

        public int ObterQuantidadeDeFilmes(string busca)
        {
            busca = busca.ToLower();

            return _auditoriaDataContext.Filmes
             .AsNoTracking()
             .Where(x => x.Titulo.Contains(busca))
             .Count();
        }

        public IList<Filme> ObterFilmesNuncaAlugados()
        {
            var filmes = _auditoriaDataContext.Filmes.Include(e => e.Locacoes).AsEnumerable();

            var filmesNuncaAlugados = filmes.Where(x => x.NuncaAlugado()).ToList();

            return filmesNuncaAlugados;
        }

        public IList<Filme> ObterIntervalo(string busca, int pular, int pegar)
        {
            busca = busca.ToLower();

            var filmes = _auditoriaDataContext.Filmes
                            .AsNoTracking()
                            .Where(x => x.Titulo.Contains(busca))
                            .Skip(pular)
                            .Take(pegar)
                            .ToList();

            return filmes;
        }

        public IList<Filme> ObterMaisAlugadosPorIntervalo(int pegar, DateTime dataInicial)
        {
            //Data final do intervalo é sempre Now.

            var filmes = _auditoriaDataContext.Filmes
                    .AsNoTracking()
                    .Include(e => e.Locacoes);

            var filmesDoIntervalo = filmes.Where(x => x.Locacoes.Any(l => l.DataDeLocacao >= dataInicial));

            var filmesOrdenadosDecresceste = filmesDoIntervalo
                .OrderByDescending(e => e.Locacoes.Count(l => l.DataDeLocacao >= dataInicial))
                .Take(pegar)
                .ToList();

            return filmesOrdenadosDecresceste;
        }

        public IList<Filme> ObterMenosAlugadosPorIntervalo(int pegar, DateTime dataInicial)
        {
            //Data final do intervalo é sempre Now.

            var filmes = _auditoriaDataContext.Filmes
                    .AsNoTracking()
                    .Include(e => e.Locacoes);

            var filmesDoIntervalo = filmes.Where(x => x.Locacoes.Any(l => l.DataDeLocacao >= dataInicial));

            var filmesOrdenadosCrescente = filmesDoIntervalo
                .OrderBy(e => e.Locacoes.Count(l => l.DataDeLocacao >= dataInicial))
                .Take(pegar)
                .ToList();

            return filmesOrdenadosCrescente;
        }

        public void Dispose()
        {
            _auditoriaDataContext.Dispose();
        }
    }
}
