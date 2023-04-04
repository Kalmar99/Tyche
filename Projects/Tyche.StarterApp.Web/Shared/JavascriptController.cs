using Microsoft.AspNetCore.Mvc;

namespace Tyche.StarterApp.Shared;

public class JavascriptController : Controller
{
    [HttpGet("js/{**staticFile}")]
    public IActionResult Index([FromRoute] string? staticFile)
    {
        if (staticFile == null || !staticFile.EndsWith("js"))
        {
            return NotFound();
        }
        
        return PhysicalFile($"{AppDomain.CurrentDomain.BaseDirectory}/{staticFile}", "text/javascript");
    }
}