# Golden Raspberry Awards API

API RESTful para ler a lista de indicados e vencedores da categoria Pior Filme do Golden Raspberry Awards.

## Sumário

- [Golden Raspberry Awards API](#golden-raspberry-awards.api)
  - [Sumário](#sumário)
  - [Instalação](#instalação)
  - [Executando a Aplicação](#executando-a-aplicação)
  - [Documentação da API](#documentação-da-api)
  - [Executando os Testes](#executando-os-testes)
  - [Estrutura do Projeto](#estrutura-do-projeto)

## Instalação

Certifique-se de que você tem o [.NET 8 SDK](https://dotnet.microsoft.com/pt-br/download) ou superior instalado em sua máquina.
- Clone o repositório e navegue até o diretório do projeto:

```sh
https://github.com/seu-usuario/golden-raspberry-awards.api.git
cd golden-raspberry-awards.api
```

## Executando a Aplicação

Para iniciar o servidor em modo de desenvolvimento:
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
dotnet test
```
## Estrutura do Projeto
- src
  - /Data: Arquivo com os dados para ser importado.
  - /Presentation:
    - /Contexts: Gerencia o contexto do Database.
    - /Endpoints: Contém as rotas da API.
    - /Extensions: Métodos de extensão para facilitar o uso de classes existentes.
    - /Models: Modelos de dados (DTOs e entidades) usados para comunicação.
    - /Responses: Estrutura e formatação das respostas da API.
    - /Services: Serviços da aplicação.
  - /Tests: Testes.
