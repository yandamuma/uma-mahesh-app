using UmaMahesh_BackApp.Models.Email;

namespace UmaMahesh_BackApp.Services.EmailService;

public interface IEmailService
{
    void SendEmail(EmailDto request);
}
