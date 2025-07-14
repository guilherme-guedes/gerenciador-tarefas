using GerenciadorTarefas.Dominio.Modelos;
using GerenciadorTarefas.Dominio.Repositorios;
using GerenciadorTarefas.Infraestrutura;
using GerenciadorTarefas.Infraestrutura.Repositorios;

namespace GerenciadorTarefas.Dominio.Servicos
{
    public class ServicoTarefa : IServicoTarefa
    {
        private readonly IRepositorioProjeto repositorioProjeto;
        private readonly IRepositorioTarefa repositorioTarefa;
        private readonly IRepositorioComentario repositorioComentario;

        public ServicoTarefa(IRepositorioProjeto repositorioProjeto, 
            IRepositorioTarefa repositorioTarefa, 
            IRepositorioComentario repositorioComentario)
        {
            this.repositorioProjeto = repositorioProjeto;
            this.repositorioTarefa = repositorioTarefa;
            this.repositorioComentario = repositorioComentario;
        }

        public async Task<int> Criar(int idProjeto, Tarefa tarefa)
        {
            var projeto = await repositorioProjeto.Obter(idProjeto);
            if (projeto is null)
                throw new KeyNotFoundException("Projeto não encontrado.");

            projeto.AdicionarNovaTarefa(tarefa);

            return await repositorioTarefa.Criar(tarefa);
        }

        public async Task<Tarefa> Atualizar(Tarefa tarefa)
        {
            if (tarefa is null)
                throw new ArgumentException("Dados de tarefa não informados");
            
            var tarefaBanco = await repositorioTarefa.Obter(tarefa.Id);
            
            if(tarefaBanco.FoiConcluida())
                throw new InvalidOperationException("Tarefa já concluída, não é possível atualizar.");

            return await repositorioTarefa.Atualizar(tarefa);
        }

        public async Task Excluir(int id)
        {
            var tarefaBanco = await repositorioTarefa.Obter(id);

            if (tarefaBanco is null)
                throw new KeyNotFoundException("Tarefa não encontrada.");

            await repositorioTarefa.Excluir(id);
        }

        public async Task<Tarefa> Obter(int id)
        {
            return await repositorioTarefa.Obter(id);
        }

        public async Task<List<Tarefa>> Listar(int idProjeto)
        {
            var projeto = await repositorioProjeto.Obter(idProjeto);
            if (projeto is null)
                throw new KeyNotFoundException("Projeto não encontrado.");

            return await repositorioTarefa.Listar(projeto);
        }

        public async Task<int> CriarComentario(int idTarefa, Comentario comentario)
        {
            var tarefa = await repositorioTarefa.Obter(idTarefa);
            if (tarefa is null)
                throw new KeyNotFoundException("Tarefa não encontrada.");

            tarefa.AdicionarComentario(comentario);

            return await repositorioComentario.Criar(comentario);
        }
    }
}