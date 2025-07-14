using GerenciadorTarefas.Dominio;
using GerenciadorTarefas.Dominio.Modelos;
using GerenciadorTarefas.Dominio.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorTarefas.Infraestrutura.Repositorios
{
    public class RepositorioUsuario : RepositorioBase, IRepositorioUsuario
    {
        private readonly ILogger<RepositorioUsuario> logger;

        public RepositorioUsuario(ContextoBanco contexto,
            IContextoUsuario contextoUsuario,
            ILogger<RepositorioUsuario> logger) : 
            base(contexto, contextoUsuario)
        {
            this.logger = logger;
        }

        public async Task<Usuario> Obter(string token)
        {
            if(string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException("Token de usuário não informado");

            return await contexto.Usuarios.FirstOrDefaultAsync(u => u.Token == token); 
        }
    }
}
