# Tarefas

# Tarefas

| ID | Tarefa | Origem | Depende de | Concluída quando |
|---|---|---|---|---|
| T-01 | Criar projeto e estrutura de pastas | Constituição: Stack e Convenções | — | A solução compila e contém `src` e `tests`. |
| T-02 | Implementar gerador de senhas | Spec: Regras de negócio; Plano: Arquitetura | T-01 | Testes unitários comprovam o tamanho mínimo, a presença de caractere especial e a ausência de espaços. |
| T-03 | Configurar persistência de senhas | Constituição: Stack; Plano: Modelo de dados | T-01 | É possível armazenar e recuperar uma senha por GUID usando SQLite e EF Core. |
| T-04 | Implementar serviço de aplicação para geração | Spec: Requisitos funcionais; Plano: Arquitetura | T-02, T-03 | O serviço coordena a geração, a associação do GUID e a persistência, retornando sucesso somente após o armazenamento. |
| T-05 | Implementar endpoint de geração | Spec: Requisitos funcionais; Plano: Contratos | T-04 | O endpoint permite gerar uma senha e retorna o GUID após a conclusão bem-sucedida da operação. |
| T-06 | Implementar serviço de aplicação para recuperação | Spec: Requisitos funcionais; Plano: Arquitetura | T-03 | O serviço recupera a senha associada a um GUID ou identifica que ela não existe. |
| T-07 | Implementar endpoint de recuperação | Spec: Requisitos funcionais e Casos de borda; Plano: Contratos | T-06 | O endpoint retorna a senha quando encontrada, `400` para GUID inválido e `404` para GUID inexistente. |
| T-08 | Implementar testes de integração dos endpoints | Constituição: Qualidade; Spec: Critérios de aceite | T-05, T-07 | Ambos os endpoints possuem testes de integração de caminho feliz. |
| T-09 | Verificar tratamento de erros e critérios de aceite | Constituição: Qualidade; Spec: Casos de borda e Critérios de aceite | T-05, T-07, T-08 | Os cenários de validação e falha de persistência são verificados e as respostas respeitam os contratos definidos. |
| T-10 | Validar a solução completa | Desafio: Definição de pronto | T-09 | A aplicação compila sem erros ou avisos, todos os testes passam e a aplicação executa sem erros. |
