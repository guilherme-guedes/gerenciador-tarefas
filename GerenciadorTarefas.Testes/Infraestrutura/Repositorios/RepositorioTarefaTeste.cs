using GerenciadorTarefas.Dominio;
using GerenciadorTarefas.Dominio.Modelos;
using GerenciadorTarefas.Dominio.Modelos.Enums;
using GerenciadorTarefas.Infraestrutura;
using GerenciadorTarefas.Infraestrutura.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Shouldly;

namespace GerenciadorTarefas.Testes.Infraestrutura.Repositorios
{
    public class RepositorioTarefaTeste
    {
        private Usuario usuario;
        private Mock<IContextoUsuario> usuarioContexto;

        public RepositorioTarefaTeste()
        {
            usuario = new Usuario() { Id = 1 };
            mockUsuarioCorrente(usuario);
        }

        [Fact]
        public async Task Deve_Atualizar_Tarefa()
        {
            var projeto = new Projeto() { Nome = "Gerenciador", Usuario = usuario };
            var tarefa = new Tarefa
            {
                Titulo = "Implementar testes",
                Descricao = "Implementar testes de unidade para serviços",
                Prioridade = EPrioridade.Baixa,
                Status = EStatus.Pendente,
                Vencimento = DateTime.Now.AddDays(1),
                Projeto = projeto
            };
            var options = new DbContextOptionsBuilder<ContextoBanco>().UseInMemoryDatabase("GerenciadorTarefasDB").Options;

            using var context = new ContextoBanco(options);
            context.Usuarios.Add(usuario);
            context.Projetos.Add(projeto);
            context.Tarefas.Add(tarefa);
            context.SaveChanges();

            var repositorio = new RepositorioTarefa(context, usuarioContexto.Object, NullLoggerFactory.Instance.CreateLogger<RepositorioTarefa>());
            var tarefaAtualizada = await repositorio.Atualizar(new Tarefa() { Id = tarefa.Id, Titulo = "Testes", Descricao = "Sem descrição", Prioridade = EPrioridade.Alta });

            tarefaAtualizada.Status.ShouldBe(EStatus.Pendente);
            tarefaAtualizada.Titulo.ShouldBe(tarefa.Titulo);
            tarefaAtualizada.Descricao.ShouldBe(tarefa.Descricao);
            tarefaAtualizada.Prioridade.ShouldBe(EPrioridade.Baixa);
        }

        private void mockUsuarioCorrente(Usuario usuariocorrente)
        {
            usuarioContexto = new Mock<IContextoUsuario>(MockBehavior.Strict);
            usuarioContexto.Setup(uc => uc.ObterUsuarioCorrente()).Returns(usuariocorrente);
        }
    }
}