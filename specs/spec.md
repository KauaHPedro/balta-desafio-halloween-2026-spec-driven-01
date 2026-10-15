# Gerador de Senhas Fortes

## Problema
- Usuários e sistemas precisam de uma forma de gerar senhas fortes que atendam a critérios mínimos de segurança e que possam ser recuperadas posteriormente quando necessário.

## Objetivo
Permitir a geração de senhas que atendam às regras de negócio definidas.

Armazenar as senhas geradas para permitir sua recuperação posterior.

Permitir a recuperação de uma senha por meio de um identificador único (GUID).

## Usuários
Clientes HTTP, incluindo outras aplicações e sistemas que precisam gerar ou recuperar senhas.

A aplicação não exige uma interface gráfica para ser utilizada.

## Histórias
Como consumidor da API, quero gerar uma senha forte para utilizá-la em uma aplicação.

Como consumidor da API, quero recuperar uma senha gerada anteriormente usando seu GUID, para acessá-la novamente quando necessário.

## Requisitos funcionais
A API deve permitir a geração de uma senha forte.

Após a geração, a API deve associar um GUID à senha e armazenar os dados necessários para sua recuperação.

Após concluir com sucesso a geração e o armazenamento, a API deve retornar o GUID ao cliente.

A API deve permitir recuperar uma senha usando o GUID associado a ela.

Quando o GUID corresponder a uma senha armazenada, a API deve retornar a senha.

Quando o GUID estiver bem formado, mas não corresponder a uma senha armazenada, a API deve retornar 404 Not Found.

Se ocorrer uma falha que impeça a conclusão da geração ou do armazenamento, a API não deve retornar uma resposta de sucesso para a operação.

## Regras de negócio
A senha gerada deve possuir pelo menos 16 caracteres.

A senha deve conter pelo menos um caractere especial.

A senha não pode conter espaços em branco.

Cada senha armazenada deve estar associada a um GUID que permita sua recuperação posterior.

## Casos de borda
Quando o GUID recebido estiver em formato inválido, a API deve retornar 400 Bad Request, utilizando ProblemDetails.

Quando o GUID estiver bem formado, mas não existir uma senha associada a ele, a API deve retornar 404 Not Found.

Quando ocorrer uma falha durante o armazenamento, a API deve retornar uma resposta de erro e não deve informar que a operação foi concluída com sucesso.

Quando a geração da senha falhar, a API não deve armazenar uma senha incompleta ou inválida.

## Fora de escopo
Interface gráfica para interação com a aplicação.

Cadastro e gerenciamento de usuários.

Funcionalidades além da geração e recuperação de senhas previstas neste desafio.

Preparação da aplicação para uso em produção, incluindo requisitos adicionais de segurança não exigidos pelo desafio.

## Critérios de aceite
Dada uma solicitação de geração concluída com sucesso, a senha gerada deve possuir pelo menos 16 caracteres, conter pelo menos um caractere especial e não conter espaços em branco.

Dada uma geração de senha bem-sucedida, a API deve armazenar a senha e retornar um GUID associado a ela.

Dado um GUID retornado durante uma geração bem-sucedida, quando o cliente solicitar a recuperação dessa senha, a API deve retornar a senha originalmente armazenada.

Dado um GUID em formato inválido, quando o cliente solicitar a recuperação, a API deve retornar 400 Bad Request com ProblemDetails.

Dado um GUID bem formado que não esteja associado a uma senha armazenada, quando o cliente solicitar a recuperação, a API deve retornar 404 Not Found.

Dada uma falha durante o armazenamento da senha, quando o cliente solicitar a geração, a API deve retornar uma resposta de erro e não indicar sucesso.

Dada uma falha na geração da senha, a API não deve armazenar dados correspondentes a uma geração incompleta ou inválida.