using Microsoft.AspNetCore.Mvc;
using LocadoraDeFilmes.Modelos;
using LocadoraDeFilmes.Repositorios.Interfaces;
using LocadoraDeFilmes.ViewModels;

namespace LocadoraDeFilmes.Controllers
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


        // GET: api/Locacao
        [HttpGet]
        public IActionResult Get(string busca, int pular = 0, int pegar = 25)
        {
            try
            {
                busca ??= string.Empty;

                var locacoes = _repositorio.ObterIntervalo(busca, pular, pegar);
                var quantidadeTotalDeLotacoes = _repositorio.ObterQuantidadeDeLocacoes(busca);

                var locacoesViewModel = locacoes.Select(e => new ObterLocacaoViewModel(e)).ToList();

                return StatusCode(
               StatusCodes.Status200OK,
               new
               {
                   locacoesTotal = quantidadeTotalDeLotacoes,
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

        // GET api/Locacao/5
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

        // POST api/Locacao
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

        // PUT api/Locacao/5
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

        // DELETE api/Locacao/5
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
