using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Platform.API.Models;

namespace Platform.API;

public class EmailSender(IOptions<SmtpSettings> smtpSettings, IOptions<EmailFromSettings> emailFromSettings) : IEmailSender<User>
{
    public async Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
    {
        var message = new MimeMessage()
        {
            Subject = "Email Confirmation",
            Body = new TextPart(TextFormat.Html) {
                Text = @$"Please confirm your email by clicking the following link: <a href=""{confirmationLink}"">Confirm Email</a>"
            }
        };
        message.To.Add(new MailboxAddress(email, email));
        
        await SendMessageAsync(message);
    }

    public async Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
    {
        var message = new MimeMessage()
        {
            Subject = "Password Reset Code",
            Body = new TextPart(TextFormat.Html) {
                Text = @$"Your password reset code is: {resetCode}"
            }
        };
        message.To.Add(new MailboxAddress(email, email));

        await SendMessageAsync(message);
    }

    public async Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
    {
        throw new NotImplementedException();
    }

    public async Task SendMessageAsync(MimeMessage message)
    {
        message.From.Add(new MailboxAddress(emailFromSettings.Value.Name, emailFromSettings.Value.Address));

        using var client = new SmtpClient();

        await client.ConnectAsync(smtpSettings.Value.Host, smtpSettings.Value.Port);
        await client.AuthenticateAsync(smtpSettings.Value.Username, smtpSettings.Value.Password);

        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}