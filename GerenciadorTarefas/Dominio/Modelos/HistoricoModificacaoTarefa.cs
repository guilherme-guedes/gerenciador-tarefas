using GerenciadorTarefas.Dominio.Modelos.Enums;
using System.Text;

namespace GerenciadorTarefas.Dominio.Modelos
{
    public class HistoricoModificacaoTarefa
    {
        public int Id { get; set; }
        public int IdTarefaModificada { get; set; }
        public EStatus Status { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Vencimento { get; set; }
        public string Comentarios { get; set; }
        public int AutorModificacaoId { get; set; }
        public virtual Usuario AutorModificacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string DescricaoAlteracao { get; set; }

        private HistoricoModificacaoTarefa()
        { }

        public static HistoricoModificacaoTarefa CriarHistoricoEdicao(Tarefa tarefaOriginal, Tarefa tarefaAlterada, Usuario autor)
        {
            var comentarios = tarefaAlterada.TemComentarios() ? string.Join(";", tarefaAlterada.Comentarios.Select(c => c.Conteudo)) : string.Empty;
            var comentariosOriginais = tarefaOriginal.TemComentarios() ? string.Join(";", tarefaOriginal.Comentarios.Select(c => c.Conteudo)) : string.Empty;

            StringBuilder alteracoes = new StringBuilder();

            if (tarefaOriginal.Titulo != tarefaAlterada.Titulo)
                alteracoes.Append($"Título alterado de '{tarefaOriginal.Titulo}' para '{tarefaAlterada.Titulo}'; ");
            if (tarefaOriginal.Descricao != tarefaAlterada.Descricao)
                alteracoes.Append($"Descrição alterada de '{tarefaOriginal.Descricao}' para '{tarefaAlterada.Descricao}'; ");
            if (tarefaOriginal.Vencimento != tarefaAlterada.Vencimento)
                alteracoes.Append($"Vencimento alterado de '{tarefaOriginal.Vencimento.ToShortDateString()}' para '{tarefaAlterada.Vencimento.ToShortDateString()}'; ");
            if (tarefaOriginal.Status != tarefaAlterada.Status)
                alteracoes.Append($"Status alterado de '{tarefaOriginal.Status}' para '{tarefaAlterada.Status}'; ");
            if (!comentarios.Equals(comentariosOriginais))
                alteracoes.Append($"Comentários alterados; ");

            return new HistoricoModificacaoTarefa
            {
                IdTarefaModificada = tarefaOriginal.Id,
                Status = tarefaAlterada.Status,
                Titulo = tarefaAlterada.Titulo,
                Descricao = tarefaAlterada.Descricao,
                Vencimento = tarefaAlterada.Vencimento,
                Comentarios = comentarios,
                DataAlteracao = DateTime.Now,
                DescricaoAlteracao = alteracoes.ToString(),
                AutorModificacao = autor,
                AutorModificacaoId = autor.Id
            };
        }

        public static HistoricoModificacaoTarefa CriarHistoricoRemocao(Tarefa tarefaRemovida, Usuario autor)
        {
            StringBuilder alteracoes = new StringBuilder($"Tarefa {tarefaRemovida.Id} ('{tarefaRemovida.Titulo}') removida;");

            return new HistoricoModificacaoTarefa
            {
                IdTarefaModificada = tarefaRemovida.Id,
                DataAlteracao = DateTime.Now,
                DescricaoAlteracao = alteracoes.ToString(),
                AutorModificacao = autor,
                AutorModificacaoId = autor.Id
            };
        }
    }
}