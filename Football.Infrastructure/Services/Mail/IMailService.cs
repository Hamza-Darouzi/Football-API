
namespace Football.Infrastructure.Services.Mail;

public interface IMailService
{
    Task SendEmail(EmailDto request);
}
