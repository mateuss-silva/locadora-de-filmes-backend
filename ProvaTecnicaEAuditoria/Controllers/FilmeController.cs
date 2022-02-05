using Microsoft.AspNetCore.Mvc;
using ProvaTecnicaEAuditoria.Modelos;
using ProvaTecnicaEAuditoria.Repositorios.Interfaces;
using ProvaTecnicaEAuditoria.Servicos.Csv;
using ProvaTecnicaEAuditoria.ViewModels;

namespace ProvaTecnicaEAuditoria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        private IFilmeRepositorio _repositorio;
        private ICsvServico _csvServico;

        public FilmeController(IFilmeRepositorio repositorio, ICsvServico csvServico)
        {
            _repositorio = repositorio;
            _csvServico = csvServico;
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

        [HttpPost]
        public IActionResult Post(IFormFile planilha)
        {
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest,
                        new
                        {
                            mensagem = "Informe um arquivo '.csv'.",
                            status = StatusCodes.Status400BadRequest
                        });


                if (planilha.FileName.EndsWith(".csv"))
                {

                    var filmes = _csvServico.ConverterPlanilhaParaFilmes(planilha.OpenReadStream());

                    var filmesValidos = _csvServico.FilmesValidos(filmes);

                    if (filmesValidos)
                    {
                        var filmesModel = filmes.Select(e => new Filme(e)).ToList();

                        _repositorio.InserirFilmes(filmesModel);

                        return StatusCode(StatusCodes.Status202Accepted,
                          new
                          {
                              mensagem = "CSV importado com sucesso.",
                              status = StatusCodes.Status202Accepted
                          });
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status400BadRequest,
                          new
                          {
                              mensagem = "Id nulo e/ou repetido, verifique e tente novamente.",
                              status = StatusCodes.Status400BadRequest
                          });
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                      new
                      {
                          mensagem = "Extensão do arquivo incorreta, informe um arquivo '.csv'.",
                          status = StatusCodes.Status400BadRequest
                      });
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 new
                 {
                     mensagem = "Erro interno no servidor",
                     status = StatusCodes.Status500InternalServerError
                 });
            }
        }
    }
}
