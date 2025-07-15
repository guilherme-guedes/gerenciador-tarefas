using GerenciadorTarefas.Aplicacao.Middlewares;
using GerenciadorTarefas.Aplicacao.Servicos;
using GerenciadorTarefas.Dominio;
using GerenciadorTarefas.Dominio.Repositorios;
using GerenciadorTarefas.Dominio.Servicos;
using GerenciadorTarefas.Infraestrutura;
using GerenciadorTarefas.Infraestrutura.DAOs;
using GerenciadorTarefas.Infraestrutura.Repositorios;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging(configure => configure.AddSimpleConsole(options =>
{
    options.TimestampFormat = "dd-MM-yyyy HH:mm:ss ";
}));

builder.Services.AddScoped<AuditoriaInterceptor>();
builder.Services.AddScoped<IContextoUsuario, ContextoUsuario>();

builder.Services.AddDbContext<ContextoBanco>((serviceProvider, options) => 
{
    options.UseSqlite($"Data Source={builder.Configuration["BancoDados"]}");
    var auditInterceptor = serviceProvider.GetRequiredService<AuditoriaInterceptor>();
    options.AddInterceptors(auditInterceptor);
});

builder.Services.AddScoped<IRepositorioHistoricoTarefa, RepositorioHistoricoTarefa>();
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddScoped<IRepositorioProjeto, RepositorioProjeto>();
builder.Services.AddScoped<IRepositorioTarefa, RepositorioTarefa>();
builder.Services.AddScoped<IRepositorioComentario, RepositorioComentario>();
builder.Services.AddScoped<IRelatorioDAO, RelatorioDAO>();

builder.Services.AddScoped<IServicoProjeto, ServicoProjeto>();
builder.Services.AddScoped<IServicoTarefa, ServicoTarefa>();
builder.Services.AddScoped<IServicoRelatorio, ServicoRelatorio>();

builder.Services.AddScoped<IProjetoServicoAplicacao, ProjetoServicoAplicacao>();
builder.Services.AddScoped<ITarefaServicoAplicacao, TarefaServicoAplicacao>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
}); 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<MiddlewareUsuario>();
app.UseMiddleware<MiddlewareException>();

app.MapControllers();

app.Run();
