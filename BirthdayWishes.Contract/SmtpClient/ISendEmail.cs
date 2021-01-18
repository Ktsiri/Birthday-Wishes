using System.Threading.Tasks;
using BirthdayWishes.Dto;

namespace BirthdayWishes.Contract.SmtpClient
{
    public interface ISendEmail
    {
        Task SendEmailAsync(EmailDto emailDto);
    }
}
