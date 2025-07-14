using GerenciadorTarefas.Dominio.Modelos;
using Shouldly;
using GerenciadorTarefas.Dominio.Modelos.Enums;

namespace GerenciadorTarefas.Testes.Dominio.Modelos
{
    public class HistoricoModificacaoTarefaTeste
    {
        [Fact]
        public void Deve_Criar_Historico_Edicao()
        {
            var autor = new Usuario { Id = 1, Nome = "Autor" };
            var tarefaOriginal = new Tarefa
            {
                Id = 10,
                Titulo = "Título Original",
                Descricao = "Descrição Original",
                Vencimento = DateTime.Today,
                Status = EStatus.Pendente
            };
            var tarefaAlterada = new Tarefa
            {
                Id = 10,
                Titulo = "Título Alterado",
                Descricao = "Descrição Alterada",
                Vencimento = DateTime.Today.AddDays(1),
                Status = EStatus.EmAndamento
            };

            var historico = HistoricoModificacaoTarefa.CriarHistoricoEdicao(tarefaOriginal, tarefaAlterada, autor);

            historico.ShouldNotBeNull();
            historico.IdTarefaModificada.ShouldBe(tarefaOriginal.Id);
            historico.AutorModificacaoId.ShouldBe(autor.Id);
            historico.Titulo.ShouldBe(tarefaAlterada.Titulo);
            historico.DescricaoAlteracao.ShouldContain("Título alterado de");
            historico.DescricaoAlteracao.ShouldContain("Descrição alterada de");
            historico.DescricaoAlteracao.ShouldContain("Vencimento alterado de");
            historico.DescricaoAlteracao.ShouldContain("Status alterado de");
        }

        [Fact]
        public void Deve_Criar_Historico_Remocao()
        {
            var autor = new Usuario { Id = 2, Nome = "Removido" };
            var tarefaRemovida = new Tarefa
            {
                Id = 20,
                Titulo = "Tarefa Removida",
                Descricao = "Descrição Removida",
                Vencimento = DateTime.Today,
                Status = EStatus.Concluida
            };

            var historico = HistoricoModificacaoTarefa.CriarHistoricoRemocao(tarefaRemovida, autor);

            historico.ShouldNotBeNull();
            historico.IdTarefaModificada.ShouldBe(tarefaRemovida.Id);
            historico.AutorModificacaoId.ShouldBe(autor.Id);
            historico.DescricaoAlteracao.ShouldContain("removida");
        }
    }
}
