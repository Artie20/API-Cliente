using API_Cliente.CasosDeUsos;
using API_Cliente.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(routing => routing.LowercaseUrls = true);
builder.Services.AddDbContext<APIClienteContext>();
builder.Services.AddDbContext<APIClienteContext >(SQLbuilder =>
{
    SQLbuilder.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql"));
});
builder.Services.AddScoped<IActualizaClientesCasoDeUso, ActualizaClientesCasoDeUso>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("MiPoliticaCORS", policy =>
    {
        policy.WithOrigins("*")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("MiPoliticaCORS");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
