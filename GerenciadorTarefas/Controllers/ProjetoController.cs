using GerenciadorTarefas.Aplicacao.DTO;
using GerenciadorTarefas.Aplicacao.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorTarefas.Controllers
{
    [Route("projetos")]
    public class ProjetoController : Controller
    {
        private readonly ILogger<ProjetoController> logger;
        private readonly IProjetoServicoAplicacao servicoProjeto;
        private readonly ITarefaServicoAplicacao servicoTarefa;

        public ProjetoController(ILogger<ProjetoController> logger, 
                                IProjetoServicoAplicacao servicoProjeto,
                                ITarefaServicoAplicacao servicoTarefa)
        {
            this.logger = logger;
            this.servicoProjeto = servicoProjeto;
            this.servicoTarefa = servicoTarefa;
        }

        [HttpPost]
        public async Task<ActionResult> Criar([FromBody] ProjetoDTO projeto)
        {
            return Created("", await servicoProjeto.Criar(projeto));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Obter(int id)
        {
            return Ok(await servicoProjeto.Obter(id)); ;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Excluir(int id)
        {
            await servicoProjeto.Excluir(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult> Listar()
        {
            return Ok(await servicoProjeto.Listar());
        }

        [HttpPost("{id}/tarefas")]
        public async Task<ActionResult> CriarTarefa(int id, [FromBody] TarefaDTO tarefa)
        {
            return Created("", await servicoProjeto.CriarTarefa(id, tarefa));
        }

        [HttpGet("{id}/tarefas")]
        public async Task<ActionResult> ListarTarefas(int id)
        {
            return Ok(await servicoTarefa.ListarTarefas(id));
        }
    }
}