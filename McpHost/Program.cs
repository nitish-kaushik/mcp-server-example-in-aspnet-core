using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


#pragma warning disable OPENAI001
builder.Services.AddChatClient(_ =>
{
    var azureOpenAiConfig = builder.Configuration.GetSection("AzureOpenAI");
    var apiKey = azureOpenAiConfig["ApiKey"];
    var endpoint = azureOpenAiConfig["Endpoint"];
    var deploymentName = azureOpenAiConfig["DeploymentName"];

    var azureClient = new AzureOpenAIClient(new Uri(endpoint!), new AzureKeyCredential(apiKey!));

    var clientProvider = azureClient.GetResponsesClient();

    return clientProvider.AsIChatClient(deploymentName);
}).UseFunctionInvocation();

#pragma warning restore OPENAI001

var mcpServerUrl = builder.Configuration.GetValue<string>("McpServerUrl");

await using var mcpClient = await McpClient
    .CreateAsync(new HttpClientTransport(new HttpClientTransportOptions
{
    Endpoint = new Uri(mcpServerUrl!)!,
}));

var tools = await mcpClient.ListToolsAsync();

builder.Services.AddSingleton<IReadOnlyList<AITool>>([.. tools.Cast<AITool>()]);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
