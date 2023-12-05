using UmaMahesh_BackApp.Entities.Custom.Email;

namespace UmaMahesh_BackApp.Features.Custom.Email;

public interface IEmailService
{
    void SendEmail(EmailDto request);
}
