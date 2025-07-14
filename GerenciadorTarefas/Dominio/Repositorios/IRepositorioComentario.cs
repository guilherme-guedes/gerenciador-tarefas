using GerenciadorTarefas.Dominio.Modelos;

namespace GerenciadorTarefas.Dominio.Repositorios
{
    public interface IRepositorioComentario
    {
        public Task<int> Criar(Comentario comentario);
    }
}
