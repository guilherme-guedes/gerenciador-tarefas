using GerenciadorTarefas.Aplicacao.DTO;

namespace GerenciadorTarefas.Aplicacao.Middlewares
{
    public class MiddlewareException
    {
        private readonly RequestDelegate next;
        private readonly ILogger<MiddlewareException> logger;

        public MiddlewareException(RequestDelegate next, ILogger<MiddlewareException> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext contextoHttp)
        {
            try
            {
                await next(contextoHttp);
            }
            catch (KeyNotFoundException kex)
            {
                logger.LogWarning(kex, kex.Message);
                await AtualizarRespostacomErro(contextoHttp, RetornoErro.CriarErroNotFound(kex.Message));
            }
            catch (ArgumentException aex)
            {
                await AtualizarRespostacomErro(contextoHttp, RetornoErro.CriarErroBadRequest(aex.Message));
            }
            catch (InvalidOperationException iex)
            {
                await AtualizarRespostacomErro(contextoHttp, RetornoErro.CriarErroBadRequest(iex.Message));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                await AtualizarRespostacomErro(contextoHttp, RetornoErro.CriarErroInterno(ex.Message));
            }
        }

        private static async Task AtualizarRespostacomErro(HttpContext contextoHttp, RetornoErro erro)
        {
            contextoHttp.Response.StatusCode = erro.Status;
            contextoHttp.Response.ContentType = "application/json";
            await contextoHttp.Response.WriteAsJsonAsync(erro);
        }
    }
}