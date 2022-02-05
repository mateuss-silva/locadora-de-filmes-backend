using Microsoft.AspNetCore.Mvc;
using ProvaTecnicaEAuditoria.Modelos;
using ProvaTecnicaEAuditoria.Repositorios;
using ProvaTecnicaEAuditoria.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProvaTecnicaEAuditoria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocacaoController : ControllerBase
    {
        private ILocacaoRepositorio _repositorio;

        public LocacaoController(ILocacaoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }


        // GET: api/<LocacaoController>
        [HttpGet]
        public IActionResult Get(string busca = "", int pular = 0, int pegar = 25)
        {
            try
            {
                var locacoes = _repositorio.ObterIntervalo(busca, pular, pegar);

                var locacoesViewModel = locacoes.Select(e => new ObterLocacaoViewModel(e)).ToList();

                return StatusCode(
               StatusCodes.Status200OK,
               new
               {
                   locacoes = locacoesViewModel,
                   mensagem = "Locações obtidas com sucesso.",
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

        // GET api/<LocacaoController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var locacao = _repositorio.ObterPorId(id);

                if (locacao == null)
                {
                    return StatusCode(
                      StatusCodes.Status404NotFound,
                      new
                      {
                          mensagem = "Locação não encontrada.",
                          status = StatusCodes.Status404NotFound
                      });
                }
                else
                {
                    var locacaoViewModel = new AtualizarLocacaoViewModel(locacao);

                    return StatusCode(
                     StatusCodes.Status200OK,
                     new
                     {
                         locacao = locacaoViewModel,
                         mensagem = "Locação obtida com sucesso.",
                         status = StatusCodes.Status200OK
                     });

                }
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

        // POST api/<LocacaoController>
        [HttpPost]
        public IActionResult Post([FromBody] InserirLocacaoViewModel locacao)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                            new
                            {
                                erros = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)),
                                mensagem = "Erro nos dados da solicitação.",
                                status = StatusCodes.Status400BadRequest
                            });
                }

                var locacaoValida = _repositorio.LocacaoValida(locacao.ClienteId, locacao.FilmeId);

                if (!locacaoValida)
                {
                    return StatusCode(
                       StatusCodes.Status404NotFound,
                       new
                       {
                           mensagem = "Cliente e/ou Filme não encontrados.",
                           status = StatusCodes.Status404NotFound
                       });
                }
                else
                {
                    var locacaoModel = new Locacao(locacao);

                    _repositorio.Inserir(locacaoModel);

                    return StatusCode(StatusCodes.Status201Created,
                               new
                               {
                                   id = locacaoModel.Id,
                                   mensagem = "Locação criado com sucesso.",
                                   status = StatusCodes.Status201Created
                               });
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   new
                   {
                       mensagem = "Erro interno no servidor",
                       status = StatusCodes.Status500InternalServerError
                   });
            }
        }

        // PUT api/<LocacaoController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] AtualizarLocacaoViewModel locacao)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                            new
                            {
                                erros = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)),
                                mensagem = "Erro nos dados da solicitação.",
                                status = StatusCodes.Status400BadRequest
                            });
                }

                var locacaoModel = _repositorio.ObterPorId(id);
                var locacaoValida = _repositorio.LocacaoValida(locacao.ClienteId, locacao.FilmeId);

                if (locacaoModel == null || !locacaoValida)
                {
                    return StatusCode(
                     StatusCodes.Status404NotFound,
                     new
                     {
                         mensagem = "Locação e/ou Filme não encontrados.",
                         status = StatusCodes.Status404NotFound
                     });
                }
                else
                {
                    locacaoModel.Atualizar(locacao);

                    _repositorio.Atualizar(locacaoModel);

                    return StatusCode(StatusCodes.Status200OK,
                       new
                       {
                           mensagem = "Locação atualizada com sucesso.",
                           status = StatusCodes.Status200OK
                       });
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   new
                   {
                       mensagem = "Erro interno no servidor",
                       status = StatusCodes.Status500InternalServerError
                   });
            }
        }

        // DELETE api/<LocaçãoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var locacao = _repositorio.ObterPorId(id);

                if (locacao == null)
                {
                    return StatusCode(
                      StatusCodes.Status404NotFound,
                      new
                      {
                          mensagem = "Locação não encontrada.",
                          status = StatusCodes.Status404NotFound
                      });
                }
                else
                {
                    _repositorio.Deletar(locacao);

                    return StatusCode(StatusCodes.Status200OK,
                         new
                         {
                             mensagem = "Locação deletada com sucesso.",
                             status = StatusCodes.Status200OK
                         });
                }
            }
            catch
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
