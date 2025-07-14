using GerenciadorTarefas.Dominio.Modelos;

namespace GerenciadorTarefas.Dominio.Servicos
{
    public interface IServicoProjeto
    {
        public Task<int> Criar(Projeto projeto);
        public Task Excluir(int id);
        public Task<Projeto> Obter(int id);
        public Task<List<Projeto>> Listar();
    }
}
