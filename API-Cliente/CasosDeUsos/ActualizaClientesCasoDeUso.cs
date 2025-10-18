using API_Cliente.Dtos;
using API_Cliente.Repositories;

namespace API_Cliente.CasosDeUsos
{
    public interface IActualizaClientesCasoDeUso
    {
        Task<ClienteDto?> Execute(ClienteDto clientes);
    }

    public class ActualizaClientesCasoDeUso : IActualizaClientesCasoDeUso
    {
        private readonly APIClienteContext _APIclientecontext;
        public ActualizaClientesCasoDeUso(APIClienteContext APIclientecontext)
        {
            _APIclientecontext = APIclientecontext;
        }
        public async Task<ClienteDto?> Execute(ClienteDto clientes)
        {
            var entity = await _APIclientecontext.Get(clientes.Id);
            if (entity == null)
            {
                return null;
            }
            entity.Nombre = clientes.Nombre;
            entity.Apellidos = clientes.Apellidos;
            entity.Correo = clientes.Correo;
            entity.Telefono = clientes.Telefono;
            entity.Direccion = clientes.Direccion;
            await _APIclientecontext.Actualizar(entity);
            return entity.ToDto();
        }
    }

}

