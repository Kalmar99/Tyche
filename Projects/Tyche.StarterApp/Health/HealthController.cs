using Microsoft.AspNetCore.Mvc;

namespace Tyche.StarterApp.Health;

[ApiController]
[Route("api/health")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public async Task<bool> IsHealthy()
    {
        return await Task.FromResult(true);
    }
}