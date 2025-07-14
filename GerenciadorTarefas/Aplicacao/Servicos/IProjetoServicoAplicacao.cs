using GerenciadorTarefas.Aplicacao.DTO;

namespace GerenciadorTarefas.Aplicacao.Servicos
{
    public interface IProjetoServicoAplicacao
    {
        public Task<int> Criar(ProjetoDTO projeto);

        public Task<ProjetoDTO> Obter(int id);

        public Task Excluir(int id);

        public Task<List<ProjetoDTO>> Listar();

        public Task<int> CriarTarefa(int id, TarefaDTO tarefa);
    }
}