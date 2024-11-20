using CertificateManagement.Server.Models;
using CertificateManagement.Server.Properties;
using Microsoft.AspNetCore.Mvc;

namespace CertificateManagement.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CertificateController(ICertificateService certificateService) : ControllerBase
{
    [HttpPost("generate")]
    public async Task<IActionResult> GenerateCertificate([FromQuery] string email)
    {
        var request = new CertificateRequest()
        {
            Email = email
        };
        var response = await certificateService.GenerateCertificateAsync(request);

        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response.Message);
    }
}