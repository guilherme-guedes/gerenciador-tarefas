using GerenciadorTarefas.Aplicacao.DTO;
using GerenciadorTarefas.Aplicacao.Servicos;
using GerenciadorTarefas.Dominio.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorTarefas.Controllers
{
    [Route("tarefas")]

    public class TarefaController : Controller
    {
        private readonly ILogger<ProjetoController> logger;
        private readonly ITarefaServicoAplicacao servicoTarefa;

        public TarefaController(ILogger<ProjetoController> logger, 
                                ITarefaServicoAplicacao servicoTarefa, IRepositorioHistoricoTarefa historicorepo)
        {
            this.logger = logger;
            this.servicoTarefa = servicoTarefa;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AtualizarTarefa(int id, [FromBody] TarefaDTO tarefa)
        {
            return Ok(await servicoTarefa.AtualizarTarefa(id, tarefa));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Excluir(int id)
        {
            await servicoTarefa.ExcluirTarefa(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Obter(int id)
        {
            return Ok(await servicoTarefa.ObterTarefa(id));
        }

        [HttpPost("{id}/comentarios")]
        public async Task<ActionResult> CriarComentario(int id, [FromBody] ComentarioDTO comentario)
        {
            return Created("", await servicoTarefa.CriarComentario(id, comentario));
        }
    }
}
