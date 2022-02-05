using Microsoft.AspNetCore.Mvc;
using ProvaTecnicaEAuditoria.Modelos;
using ProvaTecnicaEAuditoria.Repositorios.Interfaces;
using ProvaTecnicaEAuditoria.ViewModels;

namespace ProvaTecnicaEAuditoria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private IClienteRepositorio _repositorio;

        public ClienteController(IClienteRepositorio repositorio)
        {
            _repositorio = repositorio;
        }


        // GET: api/Cliente
        [HttpGet]
        public IActionResult Get(string busca = "", int pular = 0, int pegar = 25)
        {
            try
            {
                var clientes = _repositorio.ObterIntervalo(busca, pular, pegar);
                var quantidadeTotalDeClientes = _repositorio.ObterQuantidadeDeClientes(busca);

                var clientesViewModel = clientes.Select(e => new ObterClienteViewModel(e)).ToList();

                return StatusCode(
               StatusCodes.Status200OK,
               new
               {
                   clientesTotal = quantidadeTotalDeClientes,
                   clientes = clientesViewModel,
                   mensagem = "Clientes obtidos com sucesso.",
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

        /// GET api/Cliente/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            try
            {
                var cliente = _repositorio.ObterPorId(id);

                if (cliente == null)
                {
                    return StatusCode(
                      StatusCodes.Status404NotFound,
                      new
                      {
                          mensagem = "Cliente não encontrado.",
                          status = StatusCodes.Status404NotFound
                      });
                }
                else
                {
                    var clienteViewModel = new AtualizarClienteViewModel(cliente);

                    return StatusCode(
                     StatusCodes.Status200OK,
                     new
                     {
                         cliente = clienteViewModel,
                         mensagem = "Cliente obtido com sucesso.",
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

        // POST api/Cliente
        [HttpPost]
        public IActionResult Post([FromBody] AtualizarClienteViewModel cliente)
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

                var clienteModel = new Cliente(cliente);

                var cpfExistente = _repositorio.CpfExistente(cliente.Cpf);

                if (cpfExistente)
                {
                    return StatusCode(StatusCodes.Status409Conflict,
                              new
                              {
                                  mensagem = "Erro, Cpf inserido existente.",
                                  status = StatusCodes.Status409Conflict
                              });
                }
                else
                {
                    _repositorio.Inserir(clienteModel);

                    return StatusCode(StatusCodes.Status201Created,
                               new
                               {
                                   id = clienteModel.Id,
                                   mensagem = "Cliente criado com sucesso.",
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

        // PUT api/Cliente/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] AtualizarClienteViewModel cliente)
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

                var clienteModel = _repositorio.ObterPorId(id);

                if (clienteModel == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                          new
                          {
                              mensagem = "Cliente não encontrado.",
                              status = StatusCodes.Status404NotFound
                          });
                }
                else
                {
                    clienteModel.Atualizar(cliente);

                    _repositorio.Atualizar(clienteModel);

                    return StatusCode(StatusCodes.Status200OK,
                       new
                       {
                           mensagem = "Cliente atualizado com sucesso.",
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

        // DELETE api/Cliente/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var cliente = _repositorio.ObterPorId(id);

                if (cliente == null)
                {
                    return StatusCode(
                      StatusCodes.Status404NotFound,
                      new
                      {
                          mensagem = "Cliente não encontrado.",
                          status = StatusCodes.Status404NotFound
                      });
                }
                else
                {
                    _repositorio.Deletar(cliente);

                    return StatusCode(StatusCodes.Status200OK,
                         new
                         {
                             mensagem = "Cliente deletado com sucesso.",
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
