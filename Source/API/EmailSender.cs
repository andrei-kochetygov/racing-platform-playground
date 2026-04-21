using Microsoft.AspNetCore.Identity;
using Platform.API.Models;
using System.Net.Mail;

namespace Platform.API;

public class EmailSender : IEmailSender<User>
{
    public async Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
    {
        await SendMessageAsync(new MailMessage {
            From = new MailAddress("no-reply@example.com"),
            To = { new MailAddress(email) },
            Subject = "Email Confirmation",
            Body = @$"Please confirm your email by clicking the following link: <a href=""{confirmationLink}"">Confirm Email</a>",
            IsBodyHtml = true
        });
    }

    public async Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
    {
        await SendMessageAsync(new MailMessage {
            From = new MailAddress("no-reply@example.com"),
            To = { new MailAddress(email) },
            Subject = "Password Reset Code",
            Body = @$"Your password reset code is: {resetCode}",
            IsBodyHtml = true
        });
    }

    public async Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
    {
        throw new NotImplementedException();
    }

    public async Task SendMessageAsync(MailMessage message)
    {
        using var client = new SmtpClient {
            Host = "localhost",
            Port = 1025,
        };

        await client.SendMailAsync(message);
    }
}