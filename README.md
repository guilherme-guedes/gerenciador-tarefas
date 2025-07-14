# Gerenciador de Tarefas
API para operações no gerenciamento de tarefas de projetos.

## Instruções para execução
Parâmetros (Env var)
 - `BancoDados`: Caminho do banco de dados SQLite (Ex: "gerenciador-tarefas.db").

- Primeiros passos:
	0 Configurar o parâmetro do banco de dados; Executar o comando para rodar as migrações no banco SQLite: ```dotnet ef database update``` e inicializar o banco de dados;
	1 Executar a API ```dotnet run``` (na pasta onde contém o arquivo GerenciadorTarefas.csproj)

## Docker
Rodar comandos na pasta GerenciadorTarefas (onde se encontra o arquivo Dockerfile)
```
docker build -t gerenciador-tarefas .
docker run -d -p 7000:7000 -e BancoDados=<caminh-banco> gerenciador-tarefas
```

## Roadmap

### Refinamento de negócio
- Vamos bloquear ou permitir que qualquer usuário possa alterar qualquer tarefa/projeto ou vamos criar um mecanismo de limitação? (Somente dono pode apagar, alguma organização para dar acesso a um grupo de usuários?)
- Vamos paginar resultados para um carregamento mais rápido e dinâmico e utilziar filtros para tarefas?
- Vamos prevenir usuários duplicados utilizando algum identificador único real? (cpf, email)
- Vamos prevenir tarefas "duplicadas" (mesmo título e/ou descrição) no mesmo projeto?
- Poderão ter dois projetos com mesmo nome?
- Teremos um novo status 'vencida' para tarefas com vencimento atingido?

### Questões técnicas
- Implementar autenticação e autorização (utilizando middleware);
- Melhorar isolamento de camada de domínio com classes para modelos de banco e dtos/contratos para retornos da api;
- Paginação de projetos e tarefas;
- Melhorar cobertura de testes de unidade (que ficou abaixo);
