using GerenciadorTarefas.Dominio.Modelos.Enums;

namespace GerenciadorTarefas.Dominio.Modelos
{
    public class Tarefa : EntidadeBase
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Vencimento { get; set; }
        public EStatus Status { get; set; }
        public EPrioridade Prioridade { get; set; }        
        public List<Comentario> Comentarios { get; set; }
        public virtual Projeto Projeto { get; set; }

        public bool TemComentarios() => this.Comentarios?.Count > 0;

        public bool EstaEmAberto() => this.Status == EStatus.Pendente || this.Status == EStatus.EmAndamento;

        public bool FoiConcluida() => this.Status == EStatus.Concluida;

        public void AdicionarComentario(Comentario comentario)
        {
            if(this.Comentarios is null)
                this.Comentarios = new List<Comentario>();

            if (string.IsNullOrWhiteSpace(comentario?.Conteudo))
                throw new ArgumentException("Comentário não informado.");

            comentario.Tarefa = this;
            this.Comentarios.Add(comentario);
        }
    }
}
