using API_Cliente.CasosDeUsos;
using API_Cliente.Dtos;
using API_Cliente.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API_Cliente.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : Controller
    {
        private readonly APIClienteContext _APIclientecontext;
        private readonly IActualizaClientesCasoDeUso _actualizaClientesCasoDeUso;

        public ClientesController(APIClienteContext APIclientecontext, IActualizaClientesCasoDeUso actualizaClientesCasoDeUso)
        {
            _APIclientecontext = APIclientecontext;
            _actualizaClientesCasoDeUso = actualizaClientesCasoDeUso;
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ClienteDto>))]
        public async Task<IActionResult> TraeClientes()
        {
            var result = _APIclientecontext.Cliente.Select(c => c.ToDto()).ToList();
            return new OkObjectResult(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClienteDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> TraeCliente(int id)
        {
            ClienteEntity result = await _APIclientecontext.Get(id);
            return new OkObjectResult(result.ToDto());
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> EliminaCliente(int id)
        {
            var result = await _APIclientecontext.Delete(id);
            return new OkObjectResult(result);
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClienteDto))]
        public async Task<IActionResult> CreaCliente(CreaClienteDto clientes)

        {
            ClienteEntity result = await _APIclientecontext.Add(clientes);
            return new CreatedResult($"http://localhost:5142/api/clientes/{result.Id}", null);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClienteDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizaCliente(ClienteDto clientes)
        {
            ClienteDto? result = await _actualizaClientesCasoDeUso.Execute(clientes);
            if (result == null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }
    }
}
