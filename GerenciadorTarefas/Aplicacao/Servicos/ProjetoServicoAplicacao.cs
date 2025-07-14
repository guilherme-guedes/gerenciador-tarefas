using GerenciadorTarefas.Aplicacao.DTO;
using GerenciadorTarefas.Dominio.Modelos;
using GerenciadorTarefas.Dominio.Servicos;
using Mapster;

namespace GerenciadorTarefas.Aplicacao.Servicos
{
    public class ProjetoServicoAplicacao : IProjetoServicoAplicacao
    {
        private readonly IServicoProjeto servicoProjeto;
        private readonly IServicoTarefa servicoTarefa;

        public ProjetoServicoAplicacao(IServicoProjeto servicoProjeto, IServicoTarefa servicoTarefa)
        {
            this.servicoProjeto = servicoProjeto;
            this.servicoTarefa = servicoTarefa;
        }

        public async Task<int> Criar(ProjetoDTO projeto)
        {
            return await servicoProjeto.Criar(projeto.Adapt<Projeto>());
        }

        public async Task Excluir(int id)
        {
            await servicoProjeto.Excluir(id);
        }

        public async Task<ProjetoDTO> Obter(int id)
        {
            var projeto = await servicoProjeto.Obter(id);
            return projeto.Adapt<ProjetoDTO>();
        }

        public async Task<List<ProjetoDTO>> Listar()
        {
            var projetos = await servicoProjeto.Listar();
            return projetos.Adapt<List<ProjetoDTO>>();
        }

        public async Task<int> CriarTarefa(int id, TarefaDTO tarefa)
        {
            return await servicoTarefa.Criar(id, tarefa.Adapt<Tarefa>());
        }
    }
}