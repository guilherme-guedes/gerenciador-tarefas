using GerenciadorTarefas.Dominio.Modelos.Enums;

namespace GerenciadorTarefas.Dominio.Modelos
{
    public class Projeto : EntidadeBase
    {
        public string Nome { get; set; }
        public List<Tarefa> Tarefas { get; set; }
        public Usuario Usuario { get; set; }

        public Projeto()
        {
            this.Tarefas = new List<Tarefa>();
        }

        public bool TemTarefas() => this.Tarefas?.Count > 0;

        public bool TemPendencias() => this.TemTarefas() && this.Tarefas.Any(t => t.EstaEmAberto());
        
        public void AdicionarNovaTarefa(Tarefa tarefa)
        {
            if (tarefa is null)
                throw new ArgumentException("Tarefa não informada.");
            if (string.IsNullOrWhiteSpace(tarefa.Titulo))
                throw new ArgumentException("Tarefa sem título informado.");
            if (this.Tarefas.Count >= 20)
                throw new ArgumentException("Não é permitido adicionar ter mais do que 20 tarefas em um projeto.");
            
            tarefa.Status = EStatus.Pendente;
            tarefa.Projeto = this;
            this.Tarefas.Add(tarefa);
        }
    }
}
