# Constituição do projeto

## Stack
A Stack deste projeto deve usar:
- .NET 10, C#, Minimal API
- Persistência em SQLite com EF Core
- Testes com xUnit

## Arquitetura
- Endpoint não acessa banco diretamente, sempre via serviço de aplicação
- Regra de negócio não vive no endpoint nem no handler HTTP
- Nenhuma dependência nova entra sem justificativa escrita no plano

## Qualidade
- Toda regra de negócio precisa ter teste unitário
- Todo endpoint precisa ter ao menos um teste de integração de caminho feliz
- Erro de validação sempre retorna 400 com ProblemDetails

## Convenções
- Utilize as seguintes convenções do C# para nomenclatura:
Classes:	PascalCase
Métodos: PascalCase
Propriedades:	PascalCase
Variáveis locais:	camelCase
Parâmetros:	camelCase

- Na estrutura de pastas, o código fonte deve ficar dentro de src.
- Crie uma pasta própria para os testes, chamada tests.

## Governança
- Antes de adicionar uma dependência, valide se existe justificativa para tal no plano. Explique porque as tecnologias do projeto não são suficientes, e decidiremos juntos o que implementar.
- Se uma regra da constituição impedir a implementação de um requisito, me gere um alerta e não implemente. Se houver conflito entre regras, também não implemente.
Nesses casos, informe a origem do conflito e aguarde minha decisão.
- Quando houver alguma mudança na constituição, verifique os impactos no plano, na spec e nas tasks, bem como no código gerado e nos testes. Me avise de qualquer incoerência que surgir, e vamos decidir juntos o que e como refatorar.