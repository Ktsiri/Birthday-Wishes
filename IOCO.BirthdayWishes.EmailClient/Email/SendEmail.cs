using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using IOCO.BirthdayWishes.Common.Services;
using IOCO.BirthdayWishes.Contract.Common;
using IOCO.BirthdayWishes.Contract.SmtpClient;
using IOCO.BirthdayWishes.Dto;
using IOCO.BirthdayWishes.SmtpClient.Settings;

namespace IOCO.BirthdayWishes.SmtpClient.Email
{
    [RegisterClassDependency(typeof(ISendEmail))]
    sealed class SendEmail : ISendEmail
    {
        private readonly EmailSettings _emailSettings;

        public SendEmail(ISettings<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task SendEmailAsync(EmailDto emailDto)
        {

            using (System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient(_emailSettings.SmtpHost, _emailSettings.SmtpPort))
            {
                using (MailMessage mailMessage = new MailMessage())
                {
                    emailDto.HeadersList?.ForEach(x => mailMessage.Headers.Add(x.Key, x.Value));
                    mailMessage.From = new MailAddress(
                        emailDto.ReplyExpected ? _emailSettings.ReplyEmailAddress : _emailSettings.NoReplyEmailAddress);
                    mailMessage.Subject = emailDto.Subject;
                    mailMessage.IsBodyHtml = emailDto.IsBodyHtml;
                    mailMessage.Body = emailDto.Body;
                    emailDto.ToList?.ForEach(x => mailMessage.To.Add(x));
                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
        }
    }
}
