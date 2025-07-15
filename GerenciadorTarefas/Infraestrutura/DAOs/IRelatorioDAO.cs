namespace GerenciadorTarefas.Infraestrutura.DAOs
{
    public interface IRelatorioDAO
    {
        public Task<int> ConsultarMediaTarefasConcluidasUsuario(int idUsuario);
    }
}