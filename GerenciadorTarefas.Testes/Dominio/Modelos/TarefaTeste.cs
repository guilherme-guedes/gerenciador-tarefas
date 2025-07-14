using GerenciadorTarefas.Dominio.Modelos;
using GerenciadorTarefas.Dominio.Modelos.Enums;
using Shouldly;

namespace GerenciadorTarefas.Testes.Dominio.Modelos
{
    public class TarefaTeste
    {
        [Theory]
        [InlineData(EStatus.EmAndamento)]
        [InlineData(EStatus.Pendente)]
        public void Deve_Retornar_EstaEmAberto(EStatus statusTarefa)
        {
            var tarefa = new Tarefa() { Status = statusTarefa };
            tarefa.EstaEmAberto().ShouldBeTrue();
        }

        [Fact]
        public void Deve_Retornar_NAO_EstaEmAberto()
        {
            var tarefa = new Tarefa() { Status = EStatus.Concluida };
            tarefa.EstaEmAberto().ShouldBeFalse();
        }

        [Fact]
        public void Deve_AdicionarComentario()
        {
            var tarefa = new Tarefa { Titulo = "Teste", Comentarios = new List<Comentario>() };
            var comentario = new Comentario { Conteudo = "Comentário de teste" };

            tarefa.AdicionarComentario(comentario);
            tarefa.Comentarios.ShouldContain(comentario);
        }

        [Fact]
        public void Nao_Deve_AdicionarComentario_Vazio()
        {
            var tarefa = new Tarefa();
            var comentario = new Comentario { Conteudo = "" };
            Should.Throw<ArgumentException>(() => tarefa.AdicionarComentario(comentario));
        }

        [Fact]
        public void Deve_Retornar_TemComentarios()
        {
            var tarefa = new Tarefa { Comentarios = new List<Comentario> { new Comentario { Conteudo = "Comentário 1" } } };
            tarefa.TemComentarios().ShouldBeTrue();
        }

        [Fact]
        public void Deve_Retornar_NAO_TemComentarios_Vazio()
        {
            var tarefa = new Tarefa() { Comentarios = new List<Comentario>() };
            tarefa.TemComentarios().ShouldBeFalse();
        }

        [Fact]
        public void Deve_Retornar_NAO_TemComentarios_Nulo()
        {
            var tarefa = new Tarefa { Comentarios = null };
            tarefa.TemComentarios().ShouldBeFalse();
        }

        [Theory]
        [InlineData(EStatus.EmAndamento)]
        [InlineData(EStatus.Pendente)]
        public void Deve_Retornar_NAO_FoiConcluida(EStatus statusTarefa)
        {
            var tarefa = new Tarefa() { Status = statusTarefa };
            tarefa.FoiConcluida().ShouldBeFalse();
        }

        [Fact]
        public void Deve_Retornar_FoiConcluida()
        {
            var tarefa = new Tarefa { Status = EStatus.Concluida };
            tarefa.FoiConcluida().ShouldBeTrue();
        }
    }
}