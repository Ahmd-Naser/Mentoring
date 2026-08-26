using MailKit.Net.Smtp;
using MailKit.Security;
using Mentoring.Core.Settings;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using Resend;


namespace Mentoring.Application.Services;

public class EmailService(IOptions<MailSettings> mailSettings) : IEmailSender
{
    private readonly MailSettings _mailSettings = mailSettings.Value;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage();

        message.From.Add(new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail));
        message.To.Add(new MailboxAddress("", email));
        message.Subject = subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = htmlMessage
        };

        message.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();

        // الاتصال بسيرفر Google عبر منفذ 587 وبروتوكول StartTLS
        await client.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);

        // تسجيل الدخول باستخدام البريد وكلمة مرور التطبيق (App Password)
        await client.AuthenticateAsync(_mailSettings.SenderEmail, _mailSettings.Password);

        // إرسال الإيميل
        await client.SendAsync(message);

        await client.DisconnectAsync(true);


    }
}
