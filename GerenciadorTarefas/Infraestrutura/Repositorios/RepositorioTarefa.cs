using GerenciadorTarefas.Dominio;
using GerenciadorTarefas.Dominio.Modelos;
using GerenciadorTarefas.Dominio.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorTarefas.Infraestrutura.Repositorios
{
    public class RepositorioTarefa : RepositorioBase, IRepositorioTarefa
    {
        private readonly ILogger<RepositorioTarefa> logger;

        public RepositorioTarefa(ContextoBanco contexto,
            IContextoUsuario contextoUsuario,
            ILogger<RepositorioTarefa> logger) : 
            base(contexto, contextoUsuario)
        {
            this.logger = logger;
        }

        public async Task<int> Criar(Tarefa tarefa)
        {
            tarefa.Projeto = await contexto.Projetos.FirstOrDefaultAsync(p => p.Id ==  tarefa.Projeto.Id);

            await contexto.Tarefas.AddAsync(tarefa);
            await contexto.SaveChangesAsync();
            return tarefa.Id;
        }

        public async Task<Tarefa> Atualizar(Tarefa tarefa)
        {
            var tarefaBanco = await contexto.Tarefas.FirstOrDefaultAsync(t => t.Id == tarefa.Id);

            if (tarefa is null)
                throw new KeyNotFoundException("Tarefa não encontrada.");

            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Status = tarefa.Status;
            tarefaBanco.Vencimento = tarefa.Vencimento;

            await contexto.SaveChangesAsync();
            return tarefaBanco;
        }

        public async Task Excluir(int id)
        {
            var projeto = await contexto.Tarefas.FirstOrDefaultAsync(t => t.Id == id);
            contexto.Tarefas.Remove(projeto);
            contexto.SaveChanges();
        }

        public async Task<Tarefa> Obter(int id)
        {
            return await contexto.Tarefas.Include(t => t.Comentarios).AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Tarefa>> Listar(Projeto projeto)
        {
            if (projeto is null) return null;

            return await contexto.Tarefas.Include(t => t.Projeto).AsNoTracking().Where(t => t.Projeto.Id == projeto.Id).ToListAsync();
        }
    }
}