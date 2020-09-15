using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IOCO.BirthdayWishes.Contract.SmtpClient
{
    public interface ISendEmailHelper
    {
        Task<bool> SendEmailAsync(string emailAddress, string emailBody,
            string emailSubject, bool isBodyHtml, bool replyExpected);
    }
}
