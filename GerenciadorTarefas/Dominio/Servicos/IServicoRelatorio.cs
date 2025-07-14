namespace GerenciadorTarefas.Dominio.Servicos
{
    public interface IServicoRelatorio
    {
        public Task<int> ConsultarMediaTarefasConcluidasUsuarioUltimos30Dias();
    }
}