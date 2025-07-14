using GerenciadorTarefas.Dominio.Modelos.Enums;

namespace GerenciadorTarefas.Aplicacao.DTO
{
    public class TarefaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Vencimento { get; set; }
        public EStatus Status { get; set; }
        public EPrioridade Prioridade { get; set; }
        public List<ComentarioDTO> Comentarios { get; set; }
    }
}
