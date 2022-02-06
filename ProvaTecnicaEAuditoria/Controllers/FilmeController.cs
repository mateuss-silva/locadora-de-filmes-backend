using Microsoft.AspNetCore.Mvc;
using ProvaTecnicaEAuditoria.Modelos;
using ProvaTecnicaEAuditoria.Repositorios.Interfaces;
using ProvaTecnicaEAuditoria.Servicos.Csv;
using ProvaTecnicaEAuditoria.Servicos.Excel;
using ProvaTecnicaEAuditoria.Servicos.Mapeamentos;
using ProvaTecnicaEAuditoria.Uteis;
using ProvaTecnicaEAuditoria.ViewModels;

namespace ProvaTecnicaEAuditoria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        private IFilmeRepositorio _filmeRepositorio;
        private IClienteRepositorio _clienteRepositorio;
        private ICsvServico _csvServico;
        private IExcelServico _excelServico;

        public FilmeController(
            IFilmeRepositorio filmeRepositorio,
            IClienteRepositorio clienteRepositorio,
            ICsvServico csvServico,
            IExcelServico excelServico
            )
        {
            _filmeRepositorio = filmeRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _csvServico = csvServico;
            _excelServico = excelServico;
        }


        // GET: api/Filme
        [HttpGet]
        public IActionResult Get(string busca = "", int pular = 0, int pegar = 25)
        {
            try
            {
                var filmes = _filmeRepositorio.ObterIntervalo(busca, pular, pegar);
                var quantidadeTotalDeFilmes = _filmeRepositorio.ObterQuantidadeDeFilmes(busca);
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

        // GET: api/Filme/relatorio
        [HttpGet("relatorio")]
        public IActionResult Get()
        {
            try
            {
                var quantidadeDeFilmesMenosAlugados = 3;
                var quantidadeDeFilmesMaisAlugados = 5;

                var ultimaSemana = DateTime.Now.AddDays(-7);
                var ultimoAno = DateTime.Now.AddYears(-1);

                var filmesNuncaAlugados = _filmeRepositorio.ObterFilmesNuncaAlugados().Select(e => new FilmeMapeamentoDePlanilha(e));
                var filmesMaisAlugados = _filmeRepositorio.ObterMaisAlugadosPorIntervalo(quantidadeDeFilmesMaisAlugados, ultimoAno).Select(e => new FilmeMapeamentoDePlanilha(e));
                var filmesMenosAlugados = _filmeRepositorio.ObterMenosAlugadosPorIntervalo(quantidadeDeFilmesMenosAlugados, ultimaSemana).Select(e => new FilmeMapeamentoDePlanilha(e));

                var clientesEmAtraso = _clienteRepositorio.ObterClientesEmAtrasoDeDevolucao().Select(e => new ClienteMapeamentoDePlanilha(e));
                var segundoClienteQueMaisAlugou = _clienteRepositorio.ObterClientesQueMaisAlugaram(1, 1).Select(e => new ClienteMapeamentoDePlanilha(e));

                var caminhoDoRelatorio = _excelServico.GerarRelatorio(clientesEmAtraso, filmesNuncaAlugados, filmesMaisAlugados, filmesMenosAlugados, segundoClienteQueMaisAlugou).Result;

                byte[] relatorioEmBytes = ArquivoUtil.ArquivoParaBytes(caminhoDoRelatorio);

                ArquivoUtil.ApagarArquivo(caminhoDoRelatorio);

                var relatorio = File(relatorioEmBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "relatorio.xlsx");

                return StatusCode(StatusCodes.Status200OK,
                    new
                    {
                        relatorio = relatorio,
                        mensagem = "Sucesso ao gerar relatório.",

                        status = StatusCodes.Status200OK
                    });

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

                        _filmeRepositorio.InserirFilmes(filmesModel);

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
