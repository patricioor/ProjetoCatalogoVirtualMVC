Escopo Geral

💡 Criar um projeto Web para tratar com produtos e categorias que podem ser usados para criar catálogo de produtos de vendas.
Criar uma aplicação ASP .NET Core MVC no Visual Studio 2022 Community, que permita o gerenciamento de produtos e categorias;
Definir no projeto as funcionalidades para poder consultar, criar, editar e excluir (CRUD) produtos e categorias;
Definir o modelo de domínio usando classes e com propriedades e comportamentos: Product e Category;
Definir qual arquitetura a ser usada no projeto: Usar a abordagem da Clean Architecture;
Definir os padrões que iremos implementar no projeto: MVC, Respository e CQRS;
Definir os atributos para o domínio Product: Id(int, Identity), Name (string), Description(string), Price(decimal), Stock(int), Image(string);**
Definir os atributos para o domínio Category: CategoryId(int, Idetity), Name(string);**
Definir o relacionamento usado: teremos um relacionamento um para muitos(1,N) entre Categoria e Produto.
Escopo Geral - Definição das regras de negócio para “Produto”

Definir a funcionalidade para exibir os produtos;
Definir a funcionalidade para criar um novo produto;
Permitir alterar as propriedades de um produto existente(O Id do produto não poderá ser alterado);
Definir a funcionalidade para excluir um produto existente pelo seu Id;
Definir o relacionamento do produto com a categoria( Propriedade de navegação);
Não permitir a criação de um produto com estado inconsistente (criar um construtor parametrizado);
Não permitir que os atributos do produto sejam alterados externamente(setter privados);
Não permitir que os atributos Id, Stock e Price possuam valores negativos;
Não permitir que os atributos Name, Description sejam nulos ou vazios;
Permitir que o atributo Image seja null;
O atributo Name não poderá conter menos que 3 caracteres;
O atributo Description não poderá conter menos que 5 caracteres;
O atributo Image não poderá conter mais que 250 caracteres;
O atributo Image será armazenado como uma string e o seu arquivo será separado em uma pasta do projeto;
Definir a validação das regras de negócio para o domínio Produto.
Escopo Geral - Definição das regras de negócio para “Categoria”

Definir a funcionalidade para exibir as categorias;
Definir a funcionalidade para criar uma nova categoria;
Permitir alterar as propriedades de uma categoria existente(O Id da categoria não poderá ser alterado);
Definir a funcionalidade para excluir uma categoria existente pelo seu Id;
Definir o relacionamento entre categoria e produto(propriedade de navegação);
Não permitir a criação de uma categoria com estado inconsistente (criar um construtor parametrizado);
Não permitir que os atributos da categoria sejam alterados externamente(setter privados);
Não permitir que o atributo Name seja null ou vazio;
O atributo Name não poderá conter menos que 3 caracteres;
Definir a validação das regras de negócio para o domínio Categoria.
Escopo Geral - Persistência dos dados usada no projeto

Usar banco de dados relacional: SQL Server;
Usar a ferramenta ORM: Entity Framework Core;
Usar a abordagem Code-First do Entity Framework Core para criar o banco de dados e as tabelas;
Provedor do banco de dados: Microsoft.EntityFrameworkCore.SqlServer;
Ferramenta para aplicar o Migrations: Microsoft.EntityFrameworkCore.Tools;**
Desacoplar a camada de acesso a dados do ORM: Padrão Repository.
Escopo Geral - Nomenclatura

Usar a nomenclatura recomendada pela Microsoft para nomear classes, métodos, parâmetros e variáveis;

CamelCase: Em palavras compostas ou frases. a primeira letra da primeira palavra é iniciada com minúscula e unidas sem espaços. Ex: valorDoDesconto, nomeCompleto;

PascalCase: Em palavras compostas ou frases montadas com palavras, a primeira letra de cada palavra é iniciada com maiúscula. Ex: CalculaImpostoDeRenda(), ValorDoDesconto;

Idioma: Inglês.

Escopo Geral - Estrutura do Projeto

Clean Architecture: Seguir a regra de dependência;

Ciração de uma solução e 5 projetos separados em camadas com responsabilidades definidas:

CleanArchMvc: Solução

CleanArchMvc.Domain: Modelo de domínio, regras de negócio, interfaces;
CleanArchMvc.Application: Regras de domínio da aplicação, mapeamentos, serviços, DTOs, CQRS;
CleanArchMvc.Infra.Data: EF Core, contexto, configurações, migrations, Repository;
CleanArchMvc.Infra.IoC: Injeção de dependência, registro dos serviços, tempo de vida;
CleanArchMvc.WebUI: MVC, Controllers, Views, Filtros, ViewModels;
CleanArchMvc.Domain.Tests: xUnit Test Project.
O Projeto CleanArchMvc.WebUI é do tipo ASP .NET Core Web App(Model-View-Controller)

Os demais projetos serão do tipo Class Library(.NET 6.0)

Escopo Geral - Relacionamento e dependência entre os projetos

CleanArchMvc.Domain: Não possui nenhuma dependência;
CleanArchMvc.Application: Dependência com o projeto: Domain;
CleanArchMvc.Infra.Data: Dependência com o projeto: Domain;
CleanArchMvc.Infra.IoC: Dependência com os projetos: Domain, Application, Infra.Data;
CleanArchMvc.WebUI: Dependência com o projeto: Infra.IoC.
Estrutura do Projeto - Componentes distribuídos por camadas e responsabilidade
