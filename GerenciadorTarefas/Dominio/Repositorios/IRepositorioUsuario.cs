using GerenciadorTarefas.Dominio.Modelos;

namespace GerenciadorTarefas.Dominio.Repositorios
{
    public interface IRepositorioUsuario
    {
        public Task<Usuario> Obter(string token);
    }
}
