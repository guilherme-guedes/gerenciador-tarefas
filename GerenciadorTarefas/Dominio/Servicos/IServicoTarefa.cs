using GerenciadorTarefas.Dominio.Modelos;

namespace GerenciadorTarefas.Dominio.Servicos
{
    public interface IServicoTarefa
    {
        public Task<int> Criar(int idProjeto, Tarefa tarefa);

        public Task<Tarefa> Atualizar(Tarefa tarefa);

        public Task Excluir(int id);

        public Task<Tarefa> Obter(int id);

        public Task<List<Tarefa>> Listar(int idProjeto);

        public Task<int> CriarComentario(int idTarefa, Comentario comentario);
    }
}