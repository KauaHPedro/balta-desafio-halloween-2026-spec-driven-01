# Plano técnico

## Contexto
- Desenvolver uma API sem interface gráfica para geração e recuperação de senhas fortes.

A aplicação terá dois endpoints: um para gerar e armazenar uma senha, e outro para recuperá-la por GUID.

A implementação deve respeitar a constituição do projeto e os requisitos definidos em spec.md.

A aplicação utilizará .NET 10, C#, Minimal API, SQLite, EF Core e xUnit.

## Arquitetura
Os endpoints serão responsáveis por receber requisições HTTP e devolver respostas ao cliente.

Um serviço de aplicação coordenará as operações de geração e recuperação de senhas.

Um componente específico será responsável por gerar senhas e garantir o cumprimento das regras de negócio.

O serviço de aplicação utilizará o componente gerador e o acesso a dados para concluir as operações.

O acesso a dados será realizado por meio do DbContext do EF Core, sem acesso direto ao banco pelos endpoints.

Os testes unitários verificarão as regras de geração de senhas isoladamente.

Os testes de integração verificarão os endpoints e seus principais fluxos de sucesso.

## Decisões
Utilizar RandomNumberGenerator para obter aleatoriedade adequada à geração de senhas.

Gerar o GUID na aplicação, antes de persistir o registro.

Utilizar EF Core para mapear os registros de senhas para o SQLite.

Retornar sucesso na geração somente após a persistência ser concluída.

Manter a geração de senhas separada da coordenação da operação, permitindo testes unitários sem dependência do banco.

Não adicionar dependências externas sem justificativa registrada neste plano.

## Modelo de dados
Cada registro representará uma senha gerada.

O registro possuirá um GUID único utilizado para localizar a senha.

O registro armazenará a senha gerada.

O GUID será utilizado como identificador do registro.

A persistência será realizada em SQLite por meio do EF Core.

## Contratos
Gerar senha:

Entrada: nenhuma informação obrigatória do cliente.

Sucesso: retornar o GUID associado à senha armazenada.

Status de sucesso: 200 OK

Falha na geração ou persistência: retornar uma resposta de erro, sem indicar sucesso.

Recuperar senha:

Entrada: GUID da senha desejada.

Sucesso: retornar a senha associada ao GUID.

GUID malformado: retornar 400 Bad Request com ProblemDetails.

GUID válido, mas inexistente: retornar 404 Not Found

## Riscos
Gerar senha

Entrada: nenhuma informação obrigatória do cliente.

Sucesso: retornar o GUID associado à senha armazenada.

Status de sucesso: 200 OK ou 201 Created, conforme a decisão final do contrato.

Falha na geração ou persistência: retornar uma resposta de erro, sem indicar sucesso.

Recuperar senha

Entrada: GUID da senha desejada.

Sucesso: retornar a senha associada ao GUID.

GUID malformado: retornar 400 Bad Request com ProblemDetails.

GUID válido, mas inexistente: retornar 404 Not Found.

Riscos

Aleatoriedade inadequada: poderia resultar em senhas previsíveis. Mitigação: utilizar um gerador criptograficamente seguro.

Falha na persistência: poderia levar a API a informar sucesso sem conseguir recuperar a senha depois. Mitigação: retornar sucesso somente após a confirmação do armazenamento.

Regras de negócio não atendidas: uma senha poderia ter menos de 16 caracteres, não conter caracteres especiais ou conter espaços. Mitigação: testar essas propriedades e garantir que o gerador as respeite.

Falhas não cobertas pelos testes: comportamentos incorretos poderiam passar despercebidos. Mitigação: manter testes unitários para as regras e testes de integração para os endpoints.