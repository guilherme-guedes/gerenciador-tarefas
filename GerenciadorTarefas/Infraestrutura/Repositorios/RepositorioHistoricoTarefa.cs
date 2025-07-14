using GerenciadorTarefas.Dominio;
using GerenciadorTarefas.Dominio.Modelos;
using GerenciadorTarefas.Dominio.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorTarefas.Infraestrutura.Repositorios
{
    public class RepositorioHistoricoTarefa : RepositorioBase, IRepositorioHistoricoTarefa
    {
        private readonly ILogger<RepositorioComentario> logger;

        public RepositorioHistoricoTarefa(ILogger<RepositorioComentario> logger, ContextoBanco contexto)
            : base(contexto, null)
        {
            this.logger = logger;
        }

        public async Task<List<HistoricoModificacaoTarefa>> Listar(int idTarefa)
        {
            return await this.contexto.HistoricoModificacoes.Where(h => h.IdTarefaModificada == idTarefa).ToListAsync();
        }
    }
}