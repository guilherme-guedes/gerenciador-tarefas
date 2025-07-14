using GerenciadorTarefas.Dominio.Modelos;
using GerenciadorTarefas.Dominio.Repositorios;

namespace GerenciadorTarefas.Dominio.Servicos
{
    public class ServicoProjeto : IServicoProjeto
    {
        private readonly IRepositorioProjeto repositorioProjeto;

        public ServicoProjeto(IRepositorioProjeto repositorioProjeto)
        {
            this.repositorioProjeto = repositorioProjeto;
        }

        public async Task<int> Criar(Projeto projeto)
        {
            if (projeto is null)
                throw new ArgumentException("Dados de projeto não informados");
            if (string.IsNullOrWhiteSpace(projeto.Nome))
                throw new ArgumentException("Projeto sem nome informado");

            if (projeto.TemTarefas() && projeto.Tarefas.Count > 20)
                throw new ArgumentException("Não é permitido ter mais do que 20 tarefas em um projeto.");

            foreach (var tarefa in projeto.Tarefas)
                tarefa.Status = Modelos.Enums.EStatus.Pendente;

            return await repositorioProjeto.Criar(projeto);
        }

        public async Task Excluir(int id)
        {
            var projetoBanco = await repositorioProjeto.Obter(id);

            if (projetoBanco is null)
                throw new KeyNotFoundException("Projeto não encontrado.");

            if (projetoBanco.TemPendencias())
                throw new ArgumentException("O projeto possui tarefas pendentes. Conclua ou remova elas.");

            await repositorioProjeto.Excluir(id);
        }

        public async Task<Projeto> Obter(int id)
        {
            return await repositorioProjeto.Obter(id);
        }

        public async Task<List<Projeto>> Listar()
        {
            return await repositorioProjeto.Listar();
        }
    }
}