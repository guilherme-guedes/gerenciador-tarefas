using GerenciadorTarefas.Dominio.Modelos;

namespace GerenciadorTarefas.Dominio.Repositorios
{
    public interface IRepositorioProjeto
    {
        public Task<int> Criar(Projeto projeto);
        public Task Excluir(int id);
        public Task<Projeto> Obter(int id);
        public Task<List<Projeto>> Listar();
    }
}
