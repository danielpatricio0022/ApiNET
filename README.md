# API .NET
Projeto API .NET simples que realiza operações CRUD. A API consome um back-end em Node.js para a persistência e manipulação de dados.
  
### Funcionalidades
CRUD completo para gerenciar tarefas.
Consumo de API externa em Node.js.
Testes automatizados para validação da funcionalidade.
Integração contínua e entrega contínua (CI/CD) com GitHub Actions.
Projeto documentado com Swagger.

### Tecnologias Utilizadas
- .NET 8 
- Node.js 
- xUnit 
- RestSharp 
- GitHub Actions
- Swagger Api
- Prisma Orm/Sqlite
- 
**Como Rodar o Projeto**
1. Clonar o repositório:

Copy
```
  git clone https://github.com/danielpatricio0022/ApiNET.git
```

**2. Configurar a API**

  Siga as instruções para configurar o projeto.

**3. Rodar a API**

Para rodar a aplicação localmente .Net:
  ```
  dotnet run
  ```
Para rodar a aplicação localmente Node:
  ```
  npm i
  npm run dev
  ```

**4. Testes**

Para rodar os testes unitários, execute:
  ```
  dotnet test
  ```

**5. CI/CD**

Este projeto já está configurado para CI/CD com GitHub Actions. Assim que as alterações forem feitas e push para o repositório, o pipeline será executado automaticamente para build e testes.
