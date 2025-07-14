using GerenciadorTarefas.Dominio.Modelos;
using GerenciadorTarefas.Dominio.Modelos.Enums;
using Shouldly;

namespace GerenciadorTarefas.Testes.Dominio.Modelos
{
    public class ProjetoTeste
    {
        [Fact]
        public void Deve_Retornar_TemTarefas()
        {
            var projeto = new Projeto() { Tarefas = new List<Tarefa>() { new Tarefa() } };
            projeto.Tarefas.ShouldNotBeEmpty();
        }

        [Fact]
        public void Deve_Retornar_NAO_TemTarefas()
        {
            var projeto = new Projeto();
            projeto.Tarefas.ShouldBeEmpty();
        }

        [Theory]
        [InlineData(EStatus.EmAndamento)]
        [InlineData(EStatus.Pendente)]
        public void Deve_Retornar_TemPendencias(EStatus statusTarefa)
        {
            var projeto = new Projeto() { Tarefas = new List<Tarefa>() { new Tarefa() { Status = statusTarefa } } };
            projeto.TemPendencias().ShouldBeTrue();
        }

        [Fact]
        public void Deve_Retornar_NAO_TemPendencias()
        {
            var projeto = new Projeto() { Tarefas = new List<Tarefa>() { new Tarefa() { Status = EStatus.Concluida } } };
            projeto.TemPendencias().ShouldBeFalse();
        }

        [Fact]
        public void Deve_Retornar_NAO_TemPendencias_SemTarefas()
        {
            var projeto = new Projeto();
            projeto.TemPendencias().ShouldBeFalse();
        }


        [Fact]
        public void Deve_AdicionarTarefa()
        {
            var projeto = new Projeto() { Tarefas = new List<Tarefa>() { new Tarefa() { Status = EStatus.Concluida } } };
            var tarefa = new Tarefa { Titulo = "Teste", Comentarios = new List<Comentario>() };

            projeto.AdicionarNovaTarefa(tarefa);
            projeto.Tarefas.Count.ShouldBe(2);
        }

        [Fact]
        public void Nao_Deve_AdicionarTarefa_Nula()
        {
            var projeto = new Projeto() { Tarefas = new List<Tarefa>() { new Tarefa() { Status = EStatus.Concluida } } };

            Should.Throw<ArgumentException>(() => projeto.AdicionarNovaTarefa(null));
        }

        [Fact]
        public void Nao_Deve_AdicionarTarefa_SemTitulo()
        {
            var projeto = new Projeto() { Tarefas = new List<Tarefa>() { new Tarefa() { Status = EStatus.Concluida } } };

            Should.Throw<ArgumentException>(() => projeto.AdicionarNovaTarefa(new Tarefa()));
        }
    }
}