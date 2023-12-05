using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Diagnostics;

using UmaMahesh_BackApp.Entities.Custom.Email;

namespace UmaMahesh_BackApp.Features.Custom.Email;

//[System.Runtime.Versioning.SupportedOSPlatform("windows")]
public class EmailService : IEmailService
{
    private readonly IConfiguration _config;
    private readonly ILogger<EmailService> _logger;
    public EmailService(IConfiguration config, ILogger<EmailService> logger)
    {
        _config = config;
        _logger = logger;

    }
    public void SendEmail(EmailDto request)
    {

        try
        {
            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUserName").Value));
            mail.To.Add(MailboxAddress.Parse(request.ToEmail));
            mail.Subject = request.Subject;
            mail.Body = new TextPart(TextFormat.Html) { Text = request.Body };


            using var smtp = new SmtpClient();
            //smtp.Connect(_config.GetSection("EmailHost").Value , 587, SecureSocketOptions.StartTls);
            smtp.Connect(_config.GetSection("EmailHost").Value, 465, true);
            smtp.Authenticate(_config.GetSection("EmailUserName").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(mail);
            smtp.Disconnect(true);
        }
        catch (Exception ex)
        {
            //EventLog eventLog = new EventLog();
            //eventLog.Source = "MailError";
            //eventLog.WriteEntry(ex.Message.ToString(), EventLogEntryType.Error,20000);
            //Log.Error(ex.Message);
            _logger.LogError(ex, ex.Message.ToString());

        }



    }
}
