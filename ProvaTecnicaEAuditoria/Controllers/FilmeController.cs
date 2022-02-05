#nullable disable
using Microsoft.AspNetCore.Mvc;
using ProvaTecnicaEAuditoria.Modelos;
using ProvaTecnicaEAuditoria.Repositorios;
using ProvaTecnicaEAuditoria.ViewModels;

namespace ProvaTecnicaEAuditoria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        private IFilmeRepositorio _repositorio;

        public FilmeController(IFilmeRepositorio repositorio)
        {
            _repositorio = repositorio;
        }


        // GET: api/Filme
        [HttpGet]
        public IActionResult Get(string busca = "", int pular = 0, int pegar = 25)
        {
            try
            {
                var filmes = _repositorio.ObterIntervalo(busca, pular, pegar);
                var quantidadeTotalDeFilmes = _repositorio.ObterQuantidadeDeFilmes(busca);
                var filmesViewModel = filmes.Select(e => new ObterFilmeViewModel(e)).ToList();

                return StatusCode(
               StatusCodes.Status200OK,
               new
               {
                   filmesTotal = quantidadeTotalDeFilmes,
                   filmes = filmesViewModel,
                   mensagem = "Filmes obtidos com sucesso.",
                   status = StatusCodes.Status200OK
               });

            }
            catch
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        mensagem = "Erro interno no servidor",
                        status = StatusCodes.Status500InternalServerError
                    });
            }
        }
    }
}
