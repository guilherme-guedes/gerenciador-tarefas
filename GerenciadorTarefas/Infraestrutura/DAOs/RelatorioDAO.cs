using GerenciadorTarefas.Dominio;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorTarefas.Infraestrutura.DAOs
{
    public class RelatorioDAO : IRelatorioDAO
    {
        private readonly ILogger<RelatorioDAO> logger;
        private readonly ContextoBanco contexto;
        private readonly IContextoUsuario contextoUsuario;

        public RelatorioDAO(ILogger<RelatorioDAO> logger,
                                    ContextoBanco contexto,
                                    IContextoUsuario contextoUsuario)
        {
            this.logger = logger;
            this.contexto = contexto;
            this.contextoUsuario = contextoUsuario;
        }

        public async Task<int> ConsultarMediaTarefasConcluidasUsuario(int idUsuario)
        {
            var dtInicio = DateTime.Now.Date.AddDays(-30);
            var tarefasconcluidas = await contexto.Database.SqlQueryRaw<int>("SELECT COUNT(DISTINCT id_tarefa) FROM historico_modificacoes WHERE status = 2 AND autor_id = @usuarioId AND data_alteracao >= @dataInicio",
                new SqliteParameter("@usuarioId", idUsuario),
                new SqliteParameter("@dataInicio", dtInicio)
            ).ToListAsync();

            return tarefasconcluidas[0];
        }
    }
}
