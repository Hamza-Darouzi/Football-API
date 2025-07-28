

namespace Football.Infrastructure.Services.Mail;

public class MailService(IOptions<EmailSettings> email) : IMailService
{
    private readonly IOptions<EmailSettings> _email = email;

    public async Task SendEmail(EmailDto request)
    {
        try
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("طلبك عنا", "norply@talabkanna.com") );
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
           
            var builder = new BodyBuilder();
            builder.HtmlBody = $"{request.Body}";

            email.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            await smtp.ConnectAsync(
                _email.Value.Host,
                int.Parse(_email.Value.Port),
                SecureSocketOptions.StartTls
            );

            await smtp.AuthenticateAsync(
                _email.Value.Username,
                _email.Value.Password
            );

            await smtp.SendAsync(email);

            await smtp.DisconnectAsync(true);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
       
    }

}
