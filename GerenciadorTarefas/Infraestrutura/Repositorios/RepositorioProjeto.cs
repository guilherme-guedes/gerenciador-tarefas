using GerenciadorTarefas.Dominio;
using GerenciadorTarefas.Dominio.Modelos;
using GerenciadorTarefas.Dominio.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorTarefas.Infraestrutura.Repositorios
{
    public class RepositorioProjeto : RepositorioBase, IRepositorioProjeto
    {
        private readonly ILogger<RepositorioProjeto> logger;

        public RepositorioProjeto(ILogger<RepositorioProjeto> logger, 
                                ContextoBanco contexto,
                                IContextoUsuario contextoUsuario) : 
            base(contexto, contextoUsuario)
        {
            this.logger = logger;
        }

        public async Task<int> Criar(Projeto projeto)
        {
            if (projeto.TemTarefas())
            {
                for (int i = 0; i < projeto.Tarefas.Count; i++)
                {
                    if (projeto.Tarefas[i].Id > 0)
                    {
                        var tarefaBanco = await contexto.Tarefas.FirstOrDefaultAsync(t => t.Id == projeto.Tarefas[i].Id);
                        projeto.Tarefas[i] = tarefaBanco;
                    }
                }
            }

            var usuario = contextoUsuario.ObterUsuarioCorrente();
            projeto.Usuario = contexto.Usuarios.FirstOrDefault(u => u.Id == usuario.Id);

            await contexto.Projetos.AddAsync(projeto);
            await contexto.SaveChangesAsync();
            return projeto.Id;
        }

        public async Task Excluir(int id)
        {
            var projeto = await contexto.Projetos.FirstOrDefaultAsync(p => p.Id == id);
            contexto.Projetos.Remove(projeto);
            contexto.SaveChanges();
        }

        public async Task<Projeto> Obter(int id)
        {
            return await contexto.Projetos.Include(p => p.Tarefas).AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Projeto>> Listar()
        {
            if (contextoUsuario?.ObterUsuarioCorrente() is null) return null;

            return await contexto.Projetos.Include(p => p.Usuario).AsNoTracking().Where(p => p.Usuario.Id == contextoUsuario.ObterUsuarioCorrente().Id).ToListAsync();
        }
    }
}