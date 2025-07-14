using GerenciadorTarefas.Dominio;
using GerenciadorTarefas.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GerenciadorTarefas.Infraestrutura
{
    public class AuditoriaInterceptor : SaveChangesInterceptor
    {
        private readonly IContextoUsuario contextoUsuario;

        public AuditoriaInterceptor(IContextoUsuario contextoUsuario)
        {
            this.contextoUsuario = contextoUsuario;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            ProcessarAuditoriaEntidades(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
                                                                                InterceptionResult<int> result,
                                                                                CancellationToken cancellationToken = default)
        {
            ProcessarAuditoriaEntidades(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void ProcessarAuditoriaEntidades(DbContext context)
        {
            var tarefasModificadas = context.ChangeTracker
                .Entries<Tarefa>()
                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Deleted)
                .Select(e => new
                {
                    Entry = e,
                    TarefaAntes = e.OriginalValues.ToObject() as Tarefa,
                    TarefaAtual = e.State == EntityState.Modified ? e.CurrentValues.ToObject() as Tarefa : null
                })
                .ToList();

            var usuarioCorrente = this.contextoUsuario.ObterUsuarioCorrente();
            var historicos = new List<HistoricoModificacaoTarefa>();

            foreach (var item in tarefasModificadas)
            {
                switch (item.Entry.State)
                {
                    case EntityState.Modified:
                        if (!item.Entry.Collection(t => t.Comentarios).IsLoaded)
                            item.Entry.Collection(t => t.Comentarios).Load();

                        item.TarefaAntes.Comentarios = context.Set<Comentario>().Where(c => c.Tarefa.Id == item.Entry.Entity.Id).AsNoTracking().ToList();
                        item.TarefaAtual.Comentarios = item.Entry.Entity.Comentarios;
                        historicos.Add(HistoricoModificacaoTarefa.CriarHistoricoEdicao(item.TarefaAntes, item.TarefaAtual, usuarioCorrente));
                        break;

                    case EntityState.Deleted:
                        historicos.Add(HistoricoModificacaoTarefa.CriarHistoricoRemocao(item.TarefaAntes, usuarioCorrente));
                        break;
                }
            }

            context.Set<HistoricoModificacaoTarefa>().AddRange(historicos);
        }
    }
}