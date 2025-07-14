using GerenciadorTarefas.Aplicacao.DTO;
using GerenciadorTarefas.Dominio.Modelos;
using GerenciadorTarefas.Dominio.Servicos;
using Mapster;

namespace GerenciadorTarefas.Aplicacao.Servicos
{
    public class TarefaServicoAplicacao : ITarefaServicoAplicacao
    {
        private readonly IServicoTarefa servicoTarefa;

        public TarefaServicoAplicacao(IServicoTarefa servicoTarefa)
        {
            this.servicoTarefa = servicoTarefa;
        }

        public async Task<TarefaDTO> AtualizarTarefa(int idTarefa, TarefaDTO tarefa)
        {
            if (tarefa is not null) tarefa.Id = idTarefa;
            var tarefaAtualizada = await servicoTarefa.Atualizar(tarefa.Adapt<Tarefa>());
            return tarefaAtualizada.Adapt<TarefaDTO>();
        }

        public async Task ExcluirTarefa(int idTarefa)
        {
            await servicoTarefa.Excluir(idTarefa);
        }

        public async Task<TarefaDTO> ObterTarefa(int idTarefa)
        {
            var tarefa = await servicoTarefa.Obter(idTarefa);
            return tarefa.Adapt<TarefaDTO>();
        }

        public async Task<List<TarefaDTO>> ListarTarefas(int id)
        {
            var tarefas = await servicoTarefa.Listar(id);
            return tarefas.Adapt<List<TarefaDTO>>();
        }

        public async Task<int> CriarComentario(int idTarefa, ComentarioDTO comentario)
        {
            return await servicoTarefa.CriarComentario(idTarefa, comentario.Adapt<Comentario>());
        }
    }
}