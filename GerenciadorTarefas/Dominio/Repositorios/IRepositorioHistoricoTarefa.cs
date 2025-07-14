using GerenciadorTarefas.Dominio.Modelos;

namespace GerenciadorTarefas.Dominio.Repositorios
{
    public interface IRepositorioHistoricoTarefa
    {
        public Task<List<HistoricoModificacaoTarefa>> Listar(int idTarefa);
    }
}
