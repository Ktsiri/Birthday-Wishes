using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BirthdayWishes.Common.Services;
using BirthdayWishes.Contract.SmtpClient;
using BirthdayWishes.Dto;

namespace BirthdayWishes.SmtpClient.Helpers
{
    [RegisterClassDependency(typeof(ISendEmailHelper))]
    sealed class SendEmailHelper : ISendEmailHelper
    {
        private readonly ISendEmail _sendEmail;

        public SendEmailHelper(ISendEmail sendEmail)
        {
            _sendEmail = sendEmail;
        }
        public async Task<bool> SendEmailAsync(string emailAddress, string emailBody, 
            string emailSubject, bool isBodyHtml, bool replyExpected)
        {
            var successfullySent = false;

            if (emailAddress == null) return false;
            try
            {
                await _sendEmail.SendEmailAsync(new EmailDto
                {
                    Body = emailBody,
                    IsBodyHtml = isBodyHtml,
                    ReplyExpected = replyExpected,
                    Subject = emailSubject,
                    ToList = new List<string> { emailAddress }
                });

                successfullySent = true;
            }
            catch (Exception ex)
            {
                // TODO: log the exception
            }

            return successfullySent;
        }
    }
}
