using GerenciadorTarefas.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorTarefas.Infraestrutura
{
    public class ContextoBanco : DbContext
    {
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<HistoricoModificacaoTarefa> HistoricoModificacoes { get; set; }

        public ContextoBanco(DbContextOptions<ContextoBanco> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeamentos
            modelBuilder.Entity<Usuario>().ToTable("usuarios");
            modelBuilder.Entity<Usuario>().HasKey(e => e.Id);
            modelBuilder.Entity<Usuario>().Property(e => e.Id).HasColumnName("id");
            modelBuilder.Entity<Usuario>().Property(e => e.Nome).HasColumnName("nome");
            modelBuilder.Entity<Usuario>().Property(e => e.Funcao).HasColumnName("funcao");
            modelBuilder.Entity<Usuario>().Property(e => e.Token).HasColumnName("token");

            modelBuilder.Entity<Projeto>().ToTable("projetos");
            modelBuilder.Entity<Projeto>().HasKey(e => e.Id);
            modelBuilder.Entity<Projeto>().Property(e => e.Id).HasColumnName("id");
            modelBuilder.Entity<Projeto>().Property(e => e.Nome).HasColumnName("nome");

            modelBuilder.Entity<Tarefa>().ToTable("tarefas");
            modelBuilder.Entity<Tarefa>().HasKey(e => e.Id);
            modelBuilder.Entity<Tarefa>().Property(e => e.Id).HasColumnName("id");
            modelBuilder.Entity<Tarefa>().Property(e => e.Titulo).HasColumnName("titulo");
            modelBuilder.Entity<Tarefa>().Property(e => e.Descricao).HasColumnName("descricao");
            modelBuilder.Entity<Tarefa>().Property(e => e.Status).HasColumnName("status");
            modelBuilder.Entity<Tarefa>().Property(e => e.Prioridade).HasColumnName("prioridade");
            modelBuilder.Entity<Tarefa>().Property(e => e.Vencimento).HasColumnName("data_vencimento");

            modelBuilder.Entity<Comentario>().ToTable("comentarios");
            modelBuilder.Entity<Comentario>().HasKey(e => e.Id);
            modelBuilder.Entity<Comentario>().Property(e => e.Id).HasColumnName("id");
            modelBuilder.Entity<Comentario>().Property(e => e.Conteudo).HasColumnName("conteudo");

            modelBuilder.Entity<HistoricoModificacaoTarefa>().ToTable("historico_modificacoes");
            modelBuilder.Entity<HistoricoModificacaoTarefa>().HasKey(e => e.Id);
            modelBuilder.Entity<HistoricoModificacaoTarefa>().Property(e => e.Id).HasColumnName("id");
            modelBuilder.Entity<HistoricoModificacaoTarefa>().Property(e => e.AutorModificacaoId).HasColumnName("autor_id");
            modelBuilder.Entity<HistoricoModificacaoTarefa>().Property(e => e.IdTarefaModificada).HasColumnName("id_tarefa");
            modelBuilder.Entity<HistoricoModificacaoTarefa>().Property(e => e.Status).HasColumnName("status");
            modelBuilder.Entity<HistoricoModificacaoTarefa>().Property(e => e.Titulo).HasColumnName("titulo");
            modelBuilder.Entity<HistoricoModificacaoTarefa>().Property(e => e.Descricao).HasColumnName("descricao");
            modelBuilder.Entity<HistoricoModificacaoTarefa>().Property(e => e.Vencimento).HasColumnName("data_vencimento");
            modelBuilder.Entity<HistoricoModificacaoTarefa>().Property(e => e.Comentarios).HasColumnName("comentarios");
            modelBuilder.Entity<HistoricoModificacaoTarefa>().Property(e => e.DescricaoAlteracao).HasColumnName("descricao_alteracao");
            modelBuilder.Entity<HistoricoModificacaoTarefa>().Property(e => e.DataAlteracao).HasColumnName("data_alteracao");

            // Relacionamentos
            modelBuilder.Entity<Usuario>().HasMany(e => e.Projetos).WithOne(e => e.Usuario);
            modelBuilder.Entity<Usuario>().HasMany(e => e.HistoricoModificacoes).WithOne(e => e.AutorModificacao);
            modelBuilder.Entity<Projeto>().HasMany(e => e.Tarefas).WithOne(e => e.Projeto).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Tarefa>().HasMany(e => e.Comentarios).WithOne(e => e.Tarefa).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Usuario>().HasMany(e => e.Comentarios).WithOne(e => e.Autor).OnDelete(DeleteBehavior.Cascade);

            // Seed
            modelBuilder.Entity<Usuario>().HasData(
               new Usuario
               {
                   Id = 1,
                   Nome = "Gerente",
                   Token = "GRT01",
                   Funcao = Dominio.Modelos.Enums.EFuncao.Gerente                   
               },
               new Usuario
               {
                   Id = 2,
                   Nome = "Funcionário",
                   Token = "FUNC01",
                   Funcao = Dominio.Modelos.Enums.EFuncao.Operador
               }
            );
        }
    }
}