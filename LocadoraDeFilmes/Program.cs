
using LocadoraDeFilmes.Dados;
using LocadoraDeFilmes.Repositorios;
using LocadoraDeFilmes.Repositorios.Interfaces;
using LocadoraDeFilmes.Servicos.Csv;
using LocadoraDeFilmes.Servicos.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


ConfigureMvc(builder);
ConfigureServices(builder);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(option =>
{
    option.AllowAnyOrigin();
    option.AllowAnyMethod();
    option.AllowAnyHeader();

});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<EAuditoriaContexto>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.Run();

void ConfigureServices(WebApplicationBuilder builder)
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddTransient<IClienteRepositorio, ClienteRepositorio>();
    builder.Services.AddTransient<ILocacaoRepositorio, LocacaoRepositorio>();
    builder.Services.AddTransient<IFilmeRepositorio, FilmeRepositorio>();
    builder.Services.AddTransient<ICsvServico, CsvServico>();
    builder.Services.AddTransient<IExcelServico, ExcelServico>();
    builder.Services.AddDbContext<EAuditoriaContexto>(x => x.UseMySql(ServerVersion.AutoDetect(connectionString)));
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