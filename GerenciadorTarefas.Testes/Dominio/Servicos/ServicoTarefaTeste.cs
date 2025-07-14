using GerenciadorTarefas.Dominio.Modelos;
using GerenciadorTarefas.Dominio.Modelos.Enums;
using GerenciadorTarefas.Dominio.Repositorios;
using GerenciadorTarefas.Dominio.Servicos;
using Moq;
using Shouldly;

namespace GerenciadorTarefas.Testes.Dominio.Servicos
{
    public class ServicoTarefaTeste
    {
        private Mock<IRepositorioTarefa> repositorio;
        private Mock<IRepositorioProjeto> repositorioProjeto;
        private Mock<IRepositorioComentario> repositorioComentario;

        public ServicoTarefaTeste()
        {
            repositorio = new Mock<IRepositorioTarefa>();
            repositorioProjeto = new Mock<IRepositorioProjeto>();
            repositorioComentario = new Mock<IRepositorioComentario>();
        }

        [Fact]
        public async Task Deve_Obter_Tarefa()
        {
            var tarefaEsperada = new Tarefa { Id = 1, Titulo = "Teste" };
            repositorio.Setup(r => r.Obter(It.IsAny<int>())).ReturnsAsync(tarefaEsperada);

            var servico = new ServicoTarefa(repositorioProjeto.Object, repositorio.Object, repositorioComentario.Object);

            var tarefa = await servico.Obter(1);
            tarefa.ShouldNotBeNull();
            tarefa.Id.ShouldBe(tarefaEsperada.Id);
            tarefa.Titulo.ShouldBe(tarefaEsperada.Titulo);
        }

        [Fact]
        public async Task Deve_Criar_Tarefa()
        {
            var projeto = new Projeto() { Id = 1 };
            var tarefa = new Tarefa { Titulo = "Nova Tarefa" };

            MockObtencaoProjeto(projeto);
            repositorio.Setup(r => r.Criar(It.IsAny<Tarefa>())).ReturnsAsync(1);

            var servico = new ServicoTarefa(repositorioProjeto.Object, repositorio.Object, repositorioComentario.Object);
            var resultado = await servico.Criar(1, tarefa);

            resultado.ShouldBe(1);
            repositorioProjeto.Verify(r => r.Obter(1), Times.Once);
            repositorio.Verify(r => r.Criar(It.IsAny<Tarefa>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Atualizar_Tarefa()
        {
            var projeto = new Projeto() { Id = 1 };
            var tarefa = new Tarefa { Id = 1, Titulo = "Tarefa Atualizada" };
            var tarefaExistente = new Tarefa { Id = 1, Titulo = "Tarefa Original", Status = EStatus.Pendente };

            MockObtencaoProjeto(projeto);
            MockObtencaoTarefa(tarefaExistente);

            repositorio.Setup(r => r.Atualizar(It.IsAny<Tarefa>())).ReturnsAsync(tarefa);

            var servico = new ServicoTarefa(repositorioProjeto.Object, repositorio.Object, repositorioComentario.Object);
            var resultado = await servico.Atualizar(tarefa);

            resultado.ShouldNotBeNull();
            resultado.Titulo.ShouldBe("Tarefa Atualizada");
        }

        [Fact]
        public async Task Nao_Deve_Atualizar_Tarefa_JaConcluida()
        {
            var projeto = new Projeto() { Id = 1 };
            var tarefa = new Tarefa { Id = 1, Titulo = "Tarefa Atualizada" };
            var tarefaExistente = new Tarefa { Id = 1, Titulo = "Tarefa Original", Status = EStatus.Concluida };

            MockObtencaoProjeto(projeto);
            MockObtencaoTarefa(tarefaExistente);

            var servico = new ServicoTarefa(repositorioProjeto.Object, repositorio.Object, repositorioComentario.Object);

            await Should.ThrowAsync<InvalidOperationException>(() => servico.Atualizar(tarefa));
        }

        private void MockObtencaoProjeto(Projeto projeto) => repositorioProjeto.Setup(r => r.Obter(projeto.Id)).ReturnsAsync(projeto);

        private void MockObtencaoTarefa(Tarefa tarefa) => repositorio.Setup(r => r.Obter(tarefa.Id)).ReturnsAsync(tarefa);
    }
}