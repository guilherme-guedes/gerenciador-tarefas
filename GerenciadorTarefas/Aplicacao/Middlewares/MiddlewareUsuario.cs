using GerenciadorTarefas.Aplicacao.DTO;
using GerenciadorTarefas.Dominio;
using GerenciadorTarefas.Dominio.Repositorios;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GerenciadorTarefas.Aplicacao.Middlewares
{
    public class MiddlewareUsuario
    {
        private readonly RequestDelegate next;
        private readonly ILogger<MiddlewareUsuario> logger;

        public MiddlewareUsuario(RequestDelegate next, 
            ILogger<MiddlewareUsuario> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext contextoHttp, IRepositorioUsuario repositorioUsuario, IContextoUsuario contextoUsuario)
        {
            try
            {
                if (contextoHttp.Request.Headers.TryGetValue("X-TokenUsuario", out var tokenUsuario))
                {
                    var usuario = await repositorioUsuario.Obter(tokenUsuario);
                    if (usuario is null)
                        throw new Exception("Usuário não encontrado.");

                    contextoUsuario.Preencher(usuario);
                }
                else
                {
                    logger.LogWarning("Token de usuário não informado no cabeçalho da requisição.");
                    throw new ArgumentException("Token de usuário não informado no cabeçalho da requisição.");
                }

                await next(contextoHttp);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao processar o middleware de usuário: {Message}", ex.Message);
                var erro = RetornoErro.CriarErroInterno(ex.Message);
                contextoHttp.Response.StatusCode = erro.Status;
                contextoHttp.Response.ContentType = "application/json";
                await contextoHttp.Response.WriteAsJsonAsync(erro);
            }
        }
    }
}
