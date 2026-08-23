using MailKit.Security;
using Mentoring.Core.Settings;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using Resend;


namespace Mentoring.Application.Services;

public class EmailService(IResend resend,IOptions<MailSettings> mailSettings) : IEmailSender
{
    private readonly MailSettings _mailSettings = mailSettings.Value;
    private readonly IResend _resend = resend;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new EmailMessage
        {
            From = $"{_mailSettings.DisplayName} <{_mailSettings.FromEmail}>",
            To = email,
            Subject = subject,
            HtmlBody = htmlMessage
        };

        await _resend.EmailSendAsync(message);


    }
}
