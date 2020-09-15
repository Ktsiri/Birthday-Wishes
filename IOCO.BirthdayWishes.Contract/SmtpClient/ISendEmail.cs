using System.Threading.Tasks;
using IOCO.BirthdayWishes.Dto;

namespace IOCO.BirthdayWishes.Contract.SmtpClient
{
    public interface ISendEmail
    {
        Task SendEmailAsync(EmailDto emailDto);
    }
}
