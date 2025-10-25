# 🚪 API Gateway – Projeto Base

Este repositório contém a estrutura de um projeto base para um **API Gateway** desenvolvido em .NET, com arquitetura modular e integração com serviços externos via Kafka e CEP. Ideal para aplicações distribuídas e microsserviços.

## 🧱 Estrutura do Projeto

| Pasta/Arquivo                          | Descrição                                                                 |
|----------------------------------------|---------------------------------------------------------------------------|
| `APIGateway.Api`                       | Camada de apresentação (controllers, endpoints, Swagger, etc.)           |
| `APIGateway.Application`              | Regras de negócio e orquestração de serviços                             |
| `APIGateway.Domain`                   | Entidades, interfaces e contratos de domínio                             |
| `APIGateway.Infrastructure.CEPService`| Integração com serviço externo de consulta de CEP                        |
| `APIGateway.Infrastructure.Data`      | Implementação de repositórios e acesso a dados                           |
| `APIGateway.Infrastructure.Kafka`     | Configuração e uso de Kafka (Producer/Consumer)                          |
| `APIGateway.Test`                     | Testes automatizados (unitários e/ou de integração)                      |
| `APIGateway.sln`                      | Solução principal do projeto (.NET Solution)                             |
| `docker-compose.yml`                  | Arquivo para subir dependências via Docker (ex: Kafka, Mongo, etc.)      |

## 🚀 Principais Funcionalidades

- 🔌 Integração com serviços externos via Kafka
- 📦 Arquitetura em camadas (Api, Application, Domain, Infrastructure)
- 📮 Serviço de consulta de CEP
- 🧪 Testes automatizados
- 🐳 Suporte a Docker Compose para ambiente local

## 🛠 Requisitos

- [.NET 6+](https://dotnet.microsoft.com/)
- [Docker](https://www.docker.com/)
- [Kafka](https://kafka.apache.org/)
- MongoDB ou outro banco, conforme configuração

## ▶️ Como Executar

1. Clone o repositório:
   ```bash
   git https://github.com/AndreAquilau/api-gateway-dotnet.git
   cd api-gateway
   ```

2. Suba os serviços com Docker:
   ```bash
   docker-compose up -d
   ```

3. Execute a aplicação:
   ```bash
   dotnet build
   dotnet run --project APIGateway.Api
   ```

4. Acesse a API via Swagger:
   ```
   http://localhost:<porta>/swagger
   ```

## 📌 Observações

- O projeto está preparado para evoluir com novos serviços e integrações.
- O uso de Kafka permite comunicação assíncrona entre microsserviços.
- O serviço de CEP pode ser adaptado para diferentes provedores.
