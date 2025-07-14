using GerenciadorTarefas.Dominio.Modelos;

namespace GerenciadorTarefas.Dominio
{
    public interface IContextoUsuario
    {
        public void Preencher(Usuario usuario);

        public Usuario ObterUsuarioCorrente();
    }
}
