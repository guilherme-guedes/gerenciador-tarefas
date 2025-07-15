using GerenciadorTarefas.Dominio;
using GerenciadorTarefas.Dominio.Modelos;
using GerenciadorTarefas.Dominio.Modelos.Enums;
using GerenciadorTarefas.Dominio.Servicos;
using GerenciadorTarefas.Infraestrutura.DAOs;
using Moq;
using Shouldly;

namespace GerenciadorTarefas.Testes.Dominio.Servicos
{
    public class ServicoRelatorioTeste
    {
        private Mock<IContextoUsuario> usuarioContexto;
        private Mock<IRelatorioDAO> dao;

        public ServicoRelatorioTeste()
        {
            dao = new Mock<IRelatorioDAO>();
        }

        [Fact]
        public async Task Deve_Consultar_Relatorio_RendimentoUsuarioUltimos30Dias()
        {
            var usuario = new Usuario() { Id = 1, Funcao = EFuncao.Gerente };
            mockUsuarioCorrente(usuario);

            var tarefaEsperada = new Tarefa { Id = 1, Titulo = "Teste" };
            dao.Setup(r => r.ConsultarMediaTarefasConcluidasUsuario(1)).ReturnsAsync(1);

            var servico = new ServicoRelatorio(dao.Object, usuarioContexto.Object);

            var qtdTarefasConcluidas = await servico.ConsultarMediaTarefasConcluidasUsuarioUltimos30Dias(1);
            qtdTarefasConcluidas.ShouldBe(1);
        }

        [Fact]
        public async Task Nao_Deve_Consultar_Relatorio_RendimentoUsuarioUltimos30Dias_Funcionario()
        {
            var usuario = new Usuario() { Id = 1, Funcao = EFuncao.Operador };
            mockUsuarioCorrente(usuario);

            var tarefaEsperada = new Tarefa { Id = 1, Titulo = "Teste" };
            dao.Setup(r => r.ConsultarMediaTarefasConcluidasUsuario(1)).ReturnsAsync(1);

            var servico = new ServicoRelatorio(dao.Object, usuarioContexto.Object);

            await Should.ThrowAsync<InvalidOperationException>(() => servico.ConsultarMediaTarefasConcluidasUsuarioUltimos30Dias(1));
        }

        private void mockUsuarioCorrente(Usuario usuariocorrente)
        {
            usuarioContexto = new Mock<IContextoUsuario>(MockBehavior.Strict);
            usuarioContexto.Setup(uc => uc.ObterUsuarioCorrente()).Returns(usuariocorrente);
        }
    }
}