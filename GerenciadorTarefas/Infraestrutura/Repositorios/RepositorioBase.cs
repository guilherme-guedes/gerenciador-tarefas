using GerenciadorTarefas.Dominio;

namespace GerenciadorTarefas.Infraestrutura.Repositorios
{
    public abstract class RepositorioBase
    {
        protected readonly ContextoBanco contexto;
        protected readonly IContextoUsuario contextoUsuario;

        protected RepositorioBase(ContextoBanco contexto, IContextoUsuario contextoUsuario)
        {
            this.contexto = contexto;
            this.contextoUsuario = contextoUsuario;
        }
    }
}
