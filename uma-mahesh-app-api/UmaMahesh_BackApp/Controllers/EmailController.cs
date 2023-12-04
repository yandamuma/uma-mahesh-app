using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UmaMahesh_BackApp.Models.Email;
using UmaMahesh_BackApp.Services.EmailService;

namespace UmaMahesh_BackApp.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EmailController : Controller
{
    private readonly IEmailService _email;
    private readonly ILogger<EmailController> _logger;
    public EmailController(EmailService emailService , ILogger<EmailController> logger)
    {
        _email = emailService;
        _logger = logger;
    }

    [HttpPost]
    public  IActionResult SendEmail(EmailDto request)
    {
        if (request == null)
        {
            _logger.LogInformation("Email Request is not recieved correctly" );
            return BadRequest();            
        }
       _email.SendEmail(request);
        return Ok();
    }
}
