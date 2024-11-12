# Golden Raspberry Awards API

API RESTful para ler a lista de indicados e vencedores da categoria Pior Filme do Golden Raspberry Awards.

## Sumário

- [Golden Raspberry Awards API](#golden-raspberry-awards.api)
  - [Sumário](#sumário)
  - [Instalação](#instalação)
  - [Executando a Aplicação](#executando-a-aplicação)
  - [Documentação da API](#documentação-da-api)
  - [Executando os Testes](#executando-os-testes)
  - [Pipeline de CI](#pipeline-de-ci)
  - [Estrutura do Projeto](#estrutura-do-projeto)

## Instalação

Certifique-se de que você tem o [SDK do .NET 8 ou superior](https://dotnet.microsoft.com/pt-br/download) instalado em sua máquina.
- Clone o repositório e navegue até o diretório do projeto:
1. Clone o repositório:

    ```sh
    git clone https://github.com/seu-usuario/golden-raspberry-awards.api.git
    ```

2. Navegue até o diretório do projeto:

    ```sh
    cd golden-raspberry-awards.api
    ```
    
## Executando a Aplicação

Para iniciar o servidor em modo de desenvolvimento, execute os seguintes comandos:
```sh
cd src/Presentation
dotnet run
```
A API estará disponível em http://localhost:5068.

## Documentação da API
A documentação Swagger da API pode ser acessada em http://localhost:5068/doc/swagger.

## Executando os Testes
Para executar os testes:

```sh
cd .. #Retorna para a pasta src
cd Tests
dotnet test
```
## Pipeline de CI
O projeto está configurado com um pipeline de integração contínua (CI) utilizando o GitHub Actions. O pipeline executa as seguintes etapas sempre que há um Pull Request (PR) ou Merge Request:
- Build: Compila o projeto.
- Test: Executa os testes automatizados.

Essas etapas garantem que o código esteja funcionando corretamente antes de ser integrado à branch principal.

## Estrutura do Projeto
- src
  - /Data: Arquivo com os dados para ser importado.
  - /Presentation:
    - /Contexts: Gerencia o contexto do banco de dados.
    - /Endpoints: Contém as rotas da API.
    - /Extensions: Métodos de extensão para facilitar o uso de classes existentes.
    - /Models: Modelos de dados (DTOs e entidades) usados para comunicação.
    - /Responses: Estrutura e formatação das respostas da API.
    - /Services: Serviços da aplicação.
  - /Tests: Testes.
