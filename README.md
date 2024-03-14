# .NET Release Notes

The following [.NET releases](./releases.md) are currently supported:

|  Version  | Release Date | Support | Latest Patch Version | End of Support |
| :-- | :-- | :-- | :-- | :-- |
| [.NET 8](release-notes/8.0/README.md) | [November 14, 2023](https://devblogs.microsoft.com/dotnet/announcing-dotnet-8-rc-1/) | [LTS][policies] | [8.0.0-rc.1][8.0.0-rc.1] | November 10, 2026 |
| [.NET 7](release-notes/7.0/README.md) | [November 8, 2022](https://devblogs.microsoft.com/dotnet/announcing-dotnet-7/) | [STS][policies] | [7.0.11][7.0.11] | May 14, 2024 |
| [.NET 6](release-notes/6.0/README.md) | [November 8, 2021](https://devblogs.microsoft.com/dotnet/announcing-net-6/) | [LTS][policies] | [6.0.22][6.0.22]  | November 12, 2024 |


You can find release notes for all releases, including out-of-support releases, in the [release-notes](release-notes) directory.

[8.0.0-rc.1]: release-notes/8.0/preview/8.0.0-rc.1.md
[7.0.11]: release-notes/7.0/7.0.11/7.0.11.md
[6.0.22]: release-notes/6.0/6.0.22/6.0.22.md

## Release Information

* [Download .NET](https://dotnet.microsoft.com/download/dotnet)
* [Releases Index][releases-index.json] -- Index for all release channels in JSON format
* [dotnet-install scripts](https://learn.microsoft.com/dotnet/core/tools/dotnet-install-script)
* [Installation docs](https://learn.microsoft.com/dotnet/core/install/)

[releases-index.json]: https://dotnetcli.blob.core.windows.net/dotnet/release-metadata/releases-index.json
[policies]: release-policies.md



# Microserviço .Net Core em DDD + Injeção de dep. + EF, Serilog e HealthCheck

### **Todas as camadas**:


**01 - Aplicação / Application**

Camada de Application: define os trabalhos que o software deve fazer e direciona os objetos de domínio expressivos para resolver problemas.
Esta camada é responsável por fazer a(s) aplicação(s) se comunicar diretamente com o Domínio. Nela são implementados:

- Web API (HTTP)
- Contollers
- Configurações (appsettings.json)

**02 - Domínio / Domain**

Camada de domínio: responsável por representar conceitos de negócios, informações sobre a situação de negócios e as regras de negócio. 
O estado que reflete a situação de negócios é controlado e usado aqui, embora os detalhes técnicos de armazená-lo sejam delegados
à infraestrutura. Essa camada é a essência do software de negócios.
E nela nós temos:

- Entidades
- Interfaces (contratos) para Serviços e Repositórios
- Classes dos Serviços do domínio
- Validações (se necessário)

**03 - Infraestrutura / Infrastructure**

Camada que da suporte as demais camadas. 
A camada de infraestrutura é como os dados são mantidos em bancos de dados ou outro repositório persistente.
Que atualmente é dividida por duas camadas com seus respectivos conteúdos:

**Data:**

- Repositórios
- DataModel (Mapeamento)
- Persistência de dado
- Services(coisas relacionados a base de dados se houver)

**CrossCutting** _(camada que atravessa todas as outras, portando possui referência de todas elas)_:

- IoC (Inversão de controle)
- Enums
- Health Checks
- Configurações
- Validações


<br>


**Serilog:**
O Serilog provê uma forma rápida, fácil e eficiente de logging em arquivos textos, console e mais.
A sua configuração é bem simples e portável para as mais recentes plataformas .NET.


**Health check**
Health check é um middleware que fornece um endpoint que retorna o status da aplicação.
Na sua versão básica, a aplicação é considerada saudável caso retorne o código 200 (OK) para uma solicitação web.
Mas também são fornecidas bibliotecas que nos permite verificar o status de serviços utilizados pela aplicação,
como: banco de dados, sistema de mensageria, cache, logging, serviços externos ou mesmo a criação de um health check customizado.

Documentação modelo Microsoft: (https://learn.microsoft.com/pt-br/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice)
