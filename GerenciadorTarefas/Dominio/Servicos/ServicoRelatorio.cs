using GerenciadorTarefas.Infraestrutura.DAOs;

namespace GerenciadorTarefas.Dominio.Servicos
{
    public class ServicoRelatorio : IServicoRelatorio
    {
        private readonly IRelatorioDAO relatorioDAO;
        private readonly IContextoUsuario contextoUsuario;

        public ServicoRelatorio(IRelatorioDAO relatorioDAO, IContextoUsuario contextoUsuario)
        {
            this.relatorioDAO = relatorioDAO;
            this.contextoUsuario = contextoUsuario;
        }

        public async Task<int> ConsultarMediaTarefasConcluidasUsuarioUltimos30Dias(int idUsuario)
        {
            var usuario = contextoUsuario.ObterUsuarioCorrente();
            if(!usuario.Gerente())
                throw new InvalidOperationException("Apenas gerentes podem acessar este relatório.");

            return await this.relatorioDAO.ConsultarMediaTarefasConcluidasUsuario(idUsuario);
        }
    }
}
