using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AI;

namespace McpHost.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController(IChatClient chatClient, IReadOnlyList<AITool> tools) : ControllerBase
{
    [HttpPost("")]
    public async Task<IActionResult> Post([FromBody] string request, CancellationToken cancellationToken)
    {
        var response = await chatClient.GetResponseAsync(
            request,
            new ChatOptions()
            {
                Tools = [.. tools]
            },
            cancellationToken: cancellationToken);
        return Ok(response.Text);
    }
}
