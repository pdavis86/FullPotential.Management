namespace FullPotential.Management.Utilities;

using System.Text;
using Microsoft.AspNetCore.Mvc;

public class BadRequestWithReasonResult : IActionResult
{
    public string? Reason { get; set; }

    public BadRequestWithReasonResult(string? reason)
    {
        Reason = reason;
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        if (!string.IsNullOrWhiteSpace(Reason))
        {
            var bytes = Encoding.UTF8.GetBytes(Reason);
            await context.HttpContext.Response.Body.WriteAsync(bytes, 0, bytes.Length);
            await context.HttpContext.Response.Body.FlushAsync();
        }
    }
}
