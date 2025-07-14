using GerenciadorTarefas.Aplicacao.DTO;

namespace GerenciadorTarefas.Aplicacao.Servicos
{
    public interface ITarefaServicoAplicacao
    {
        public Task<TarefaDTO> AtualizarTarefa(int idTarefa, TarefaDTO tarefa);

        public Task ExcluirTarefa(int idTarefa);

        public Task<TarefaDTO> ObterTarefa(int idTarefa);

        public Task<List<TarefaDTO>> ListarTarefas(int id);

        public Task<int> CriarComentario(int idTarefa, ComentarioDTO comentario);
    }
}