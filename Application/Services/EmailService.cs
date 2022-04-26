using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public static class EmailService
    {
        public static async Task SendEmailAsync(string email)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("CovidHelper", "testte5ttest.testtest@yandex.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = "НАПОМИНАНИЕ";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "Не забудьте принять лекарства!!!"
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.yandex.ru", 465, true);
                await client.AuthenticateAsync("testte5ttest.testtest@yandex.ru", "vrmoyyrrupnuqdfi");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
