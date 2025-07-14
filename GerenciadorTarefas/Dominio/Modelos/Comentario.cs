namespace GerenciadorTarefas.Dominio.Modelos
{
    public class Comentario : EntidadeBase
    {
        public string Conteudo { get; set; }
        public Usuario Autor { get; set; }
        public Tarefa Tarefa { get; set; }
    }
}
