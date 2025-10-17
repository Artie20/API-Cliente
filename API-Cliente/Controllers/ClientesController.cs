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

        public ClientesController(APIClienteContext APIclientecontext)
        {
            _APIclientecontext = APIclientecontext;
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
        public async Task<bool> EliminaCliente(int id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
