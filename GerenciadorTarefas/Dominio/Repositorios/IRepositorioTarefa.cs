using GerenciadorTarefas.Dominio.Modelos;

namespace GerenciadorTarefas.Dominio.Repositorios
{
    public interface IRepositorioTarefa
    {
        public Task<int> Criar(Tarefa tarefa);
        public Task<Tarefa> Atualizar(Tarefa tarefa);
        public Task Excluir(int id);
        public Task<Tarefa> Obter(int id);
        public Task<List<Tarefa>> Listar(Projeto projeto);
    }
}
