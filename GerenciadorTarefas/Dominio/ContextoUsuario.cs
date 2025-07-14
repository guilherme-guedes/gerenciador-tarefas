using GerenciadorTarefas.Dominio.Modelos;

namespace GerenciadorTarefas.Dominio
{
    public class ContextoUsuario : IContextoUsuario
    {
        private Usuario usuario;

        public void Preencher(Usuario usuario) => this.usuario = usuario;

        public Usuario ObterUsuarioCorrente() => this.usuario;
    }
}
