using GerenciadorTarefas.Dominio;
using GerenciadorTarefas.Dominio.Modelos;
using GerenciadorTarefas.Dominio.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorTarefas.Infraestrutura.Repositorios
{
    public class RepositorioComentario : RepositorioBase, IRepositorioComentario
    {
        private readonly ILogger<RepositorioComentario> logger;

        public RepositorioComentario(ILogger<RepositorioComentario> logger, 
                                    ContextoBanco contexto, 
                                    IContextoUsuario contextoUsuario) 
            : base(contexto, contextoUsuario)
        {
            this.logger = logger;
        }

        public async Task<int> Criar(Comentario comentario)
        {
            comentario.Tarefa = await contexto.Tarefas.FirstOrDefaultAsync(x => x.Id == comentario.Tarefa.Id);

            var usuario = contextoUsuario.ObterUsuarioCorrente();
            comentario.Autor = contexto.Usuarios.FirstOrDefault(u => u.Id == usuario.Id);

            contexto.Entry<Tarefa>(comentario.Tarefa).State = EntityState.Modified;
            await contexto.Comentarios.AddAsync(comentario);
            await contexto.SaveChangesAsync();
            return comentario.Id;
        }
    }
}
