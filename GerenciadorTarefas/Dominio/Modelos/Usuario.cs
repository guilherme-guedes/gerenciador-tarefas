using GerenciadorTarefas.Dominio.Modelos.Enums;

namespace GerenciadorTarefas.Dominio.Modelos
{
    public class Usuario : EntidadeBase
    {
        public string Nome { get; set; }
        public string Token { get; set; }
        public EFuncao Funcao { get; set; }
        public List<Projeto> Projetos { get; set; }
        public virtual List<HistoricoModificacaoTarefa> HistoricoModificacoes { get; set; }
        public virtual List<Comentario> Comentarios { get; set; }

        public bool Gerente() => this.Funcao == EFuncao.Gerente;
    }
}
