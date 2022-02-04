
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvaTecnicaEAuditoria.Dados;
using ProvaTecnicaEAuditoria.Repositorios;

var builder = WebApplication.CreateBuilder(args);


ConfigureMvc(builder);
ConfigureServices(builder);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureServices(WebApplicationBuilder builder)
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddTransient<IClienteRepositorio, ClienteRepositorio>();
    builder.Services.AddTransient<ILocacaoRepositorio, LocacaoRepositorio>();
    builder.Services.AddDbContext<EAuditoriaDbContext>(x => x.UseMySql(ServerVersion.AutoDetect(connectionString)));
}

void ConfigureMvc(WebApplicationBuilder builder)
{
    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.InvalidModelStateResponseFactory = actionContext =>
        new BadRequestObjectResult(
            new
            {
                error = string.Join(
                    Environment.NewLine,
                    actionContext.ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList()
                )
            }
        );

        options.SuppressModelStateInvalidFilter = true;
    });
}
