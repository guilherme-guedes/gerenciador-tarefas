using GerenciadorTarefas.Dominio.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorTarefas.Controllers
{
    [Route("relatorios")]
    public class RelatorioController : Controller
    {
        private readonly ILogger<RelatorioController> logger;
        private readonly IServicoRelatorio servicoRelatorio;

        public RelatorioController(ILogger<RelatorioController> logger,
                                IServicoRelatorio servicoRelatorio)
        {
            this.logger = logger;
            this.servicoRelatorio = servicoRelatorio;
        }

        [HttpGet("rendimento-usuario-ultimos-30dias")]
        public async Task<ActionResult> ConsultarRendimentoUsuarioUltimos30Dias()
        {
            return Ok(await servicoRelatorio.ConsultarMediaTarefasConcluidasUsuarioUltimos30Dias());
        }
    }
}