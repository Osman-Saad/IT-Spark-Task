using MailKit.Net.Smtp;
using MimeKit;

namespace ITSparkTask.PL.Helpers
{
    public class EmailService
    {
        public static async Task SendEmailAsync(string to,string subject,string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("ITSpark", "osmansaad712@gmail.com"));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart("plain")
            {
                Text = body
            };
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("osmansaad712@gmail.com", "rrhx ydro mbxj rzfs");
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
