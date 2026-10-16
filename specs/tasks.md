# Tarefas

| ID | Tarefa | Origem | Depende de | Concluída quando |
|---|---|---|---|---|
| T-01 | Criar projeto e estrutura de pastas | Constituição: Stack e Convenções | — | A solução compila e contém `src` e `tests`. |
| T-02 | Implementar gerador de senhas | Spec: Regras de negócio; Plano: Arquitetura | T-01 | Testes unitários comprovam o tamanho mínimo, a presença de caractere especial e a ausência de espaços. |
| T-03 | Configurar persistência de senhas | Constituição: Stack; Plano: Modelo de dados | T-01 | É possível armazenar e recuperar uma senha por GUID usando SQLite e EF Core. |
| T-04 | Implementar serviço de aplicação para geração | Spec: Requisitos funcionais; Plano: Arquitetura | T-02, T-03 | O serviço coordena a geração, a associação do GUID e a persistência, retornando sucesso somente após o armazenamento. |
| T-05 | Implementar serviço de aplicação para recuperação | Spec: Requisitos funcionais; Plano: Arquitetura | T-03 | O serviço recupera a senha associada a um GUID ou identifica que ela não existe. |
| T-06 | Escrever testes unitários do serviço de aplicação | Constituição: Qualidade; Plano: Arquitetura | T-04, T-05 | Os testes cobrem a geração com persistência e a recuperação por GUID (existente e inexistente) sem banco real. |
| T-07 | Configurar composição/DI e tratamento de erros | Constituição: Arquitetura e Qualidade; Plano: Arquitetura | T-04, T-05 | Gerador, serviço e DbContext são injetados; falhas não tratadas retornam `500` com ProblemDetails. |
| T-08 | Implementar endpoint de geração | Spec: Requisitos funcionais; Plano: Contratos | T-07 | O endpoint `POST /senhas` gera uma senha e retorna `200` com o GUID após a conclusão bem-sucedida. |
| T-09 | Implementar endpoint de recuperação | Spec: Requisitos funcionais e Casos de borda; Plano: Contratos | T-07 | O endpoint `GET /senhas/{id}` retorna a senha (`200`), `400` para GUID inválido e `404` para GUID inexistente. |
| T-10 | Implementar testes de integração dos endpoints | Constituição: Qualidade; Spec: Critérios de aceite | T-08, T-09 | Ambos os endpoints possuem testes de integração de caminho feliz. |
| T-11 | Verificar tratamento de erros e critérios de aceite | Constituição: Qualidade; Spec: Casos de borda e Critérios de aceite | T-09, T-10 | Os cenários de validação (`400`/ProblemDetails) e de GUID inexistente (`404`) são verificados e as respostas respeitam os contratos definidos. |
| T-12 | Validar a solução completa | Constituição: Qualidade; Desafio: Definição de pronto | T-11 | A aplicação compila sem erros ou avisos, todos os testes passam e a aplicação executa sem erros. |
