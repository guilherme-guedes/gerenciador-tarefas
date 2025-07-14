using GerenciadorTarefas.Infraestrutura.DAOs;

namespace GerenciadorTarefas.Dominio.Servicos
{
    public class ServicoRelatorio : IServicoRelatorio
    {
        private readonly RelatorioDAO relatorioDAO;

        public ServicoRelatorio(RelatorioDAO relatorioDAO)
        {
            this.relatorioDAO = relatorioDAO;
        }

        public async Task<int> ConsultarMediaTarefasConcluidasUsuarioUltimos30Dias()
        {
            return await this.relatorioDAO.ConsultarMediaTarefasConcluidasUsuario();
        }
    }
}
