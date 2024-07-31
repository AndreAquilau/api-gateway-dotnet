## Modo de Desenvolvimento

Antes de iniciar o desenvolvimento das novas features, é necessário configurar o ambiente de desenvolvimento.

Para iniciar o projeto em modo de desenvolvimento, siga os passos abaixo:

Acesse a pasta src no terminal.
Execute o seguinte comando:

```cs
dotnet watch run --project .\APIGateway.Api\ --environment Development

dotnet watch 🚀 Started
info: APIGateway.Worker.Worker[0]
      Worker running at: 07/31/2024 00:27:43 -04:00
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5254
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: C:\Projetos\.NET\APIGateway\src\APIGateway.Api
info: APIGateway.Worker.Worker[0]
      Worker running at: 07/31/2024 00:27:44 -04:00
info: APIGateway.Worker.Worker[0]
      Worker running at: 07/31/2024 00:27:45 -04:00
info: APIGateway.Worker.Worker[0]
      Worker running at: 07/31/2024 00:27:46 -04:00
```