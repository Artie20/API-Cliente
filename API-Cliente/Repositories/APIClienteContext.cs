using API_Cliente.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics;

namespace API_Cliente.Repositories
{
    public class APIClienteContext : DbContext
    {
        public APIClienteContext(DbContextOptions<APIClienteContext> options) : base(options)
        {
        }

        public DbSet<ClienteEntity> Cliente { get; set; } //Para que ya acceda a los campos de datos.

        public async Task<ClienteEntity>Get(int id)
        {
            return await Cliente.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<bool> Delete(int id)
        {
            ClienteEntity? entity = await Get(id);
            if (entity == null)
            {
                return false;
            }
            else
            {
                Cliente.Remove(entity);
                SaveChanges();
                return true;
            }
        }

        public async Task<ClienteEntity> Add(CreaClienteDto ClienteDto) //Se hace público y asíncrono Trae a Clientes de Entity y se agrega Add en CrearClientesDto clientesDto
        {
            ClienteEntity entity = new ClienteEntity() // Se asigna clientes Entity a un nuevo elemento
            {
                Id = null, //Id es igual a null.
                Nombre = ClienteDto.Nombre, //Nombre se le asigna Nombre en Dto.
                Apellidos = ClienteDto.Apellidos, //Apellidos se le asigna Apellidos en Dto.
                Correo = ClienteDto.Correo, //Correo se le asigna Correo en Dto.
                Telefono = ClienteDto.Telefono, //Telefono se le asigna Telefono en Dto.
                Direccion = ClienteDto.Direccion, //Direccion se le asigna Direccion en Dto.
            };
            EntityEntry<ClienteEntity> response = await Cliente.AddAsync(entity);
            await SaveChangesAsync();
            return await Get(response.Entity.Id ?? throw new Exception("No se ha podido Guardar."));
        
        }

        public async Task<bool> Actualizar(ClienteEntity clienteEntity)
        {
            Cliente.Update(clienteEntity);
            await SaveChangesAsync();
            return true;
        }
    }
    public class ClienteEntity
    {
        public int? Id { get; set; } //Nota se pone ? ya que puede ser null.
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }

        public ClienteDto ToDto()
        {
            return new ClienteDto
            {
                Id = Id ?? throw new Exception("El Id No Debe Ser Nulo."),
                Nombre = Nombre,
                Apellidos = Apellidos,
                Correo = Correo,
                Telefono = Telefono,
                Direccion = Direccion,
            };
        }
    }
}
