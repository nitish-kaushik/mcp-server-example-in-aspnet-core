# Build an MCP Server with ASP.NET Core: Model Context Protocol Tutorial & AI Example

## Watch the tutorial

### Build an ASP.NET Core MCP server and connect it to an AI chat app

<p align="center">
  <a href="https://www.youtube.com/watch?v=1ApBbseQzwo">
    <img src="https://img.youtube.com/vi/1ApBbseQzwo/maxresdefault.jpg" alt="Watch the Model Context Protocol tutorial: build an MCP server with ASP.NET Core and connect it to an AI chat app" width="800">
  </a>
  <br>
  ▶️ <strong><a href="https://www.youtube.com/watch?v=1ApBbseQzwo">Watch the full tutorial on YouTube</a></strong> · Click the thumbnail to play.
</p>

This repository demonstrates how to build an MCP server with ASP.NET Core and connect it to an AI-powered chat application.

## What you'll find

- **McpServer** exposes employee tools over the MCP HTTP transport:
  - `GetEmployees` returns the sample employees.
  - `GetEmployeeById` returns an employee by ID.
- **McpHost** connects to the MCP server, discovers its tools, and makes them available to an Azure OpenAI chat client.
- `POST /api/chat` accepts a JSON string containing a chat request, such as `"List all employees"`.

## Prerequisites

- .NET 11 SDK
- An Azure OpenAI endpoint, API key, and deployed model

## Run the example

1. Configure Azure OpenAI credentials for `McpHost` using .NET user secrets:

   ```sh
   dotnet user-secrets set "AzureOpenAI:ApiKey" "<your-api-key>" --project McpHost
   dotnet user-secrets set "AzureOpenAI:Endpoint" "https://<your-resource>.openai.azure.com/" --project McpHost
   dotnet user-secrets set "AzureOpenAI:DeploymentName" "<your-deployment-name>" --project McpHost
   ```

2. Start the MCP server:

   ```sh
   dotnet run --project McpServer --launch-profile https
   ```

   The server listens at `https://localhost:7038`, with its MCP endpoint at `https://localhost:7038/mcp`.

3. In a separate terminal, start the chat host:

   ```sh
   dotnet run --project McpHost --launch-profile http
   ```

4. Send a chat request:

   ```sh
   curl -X POST http://localhost:5006/api/chat \
     -H "Content-Type: application/json" \
     -d '"List all employees"'
   ```

The host uses the MCP tools to answer questions about the sample employee data.
