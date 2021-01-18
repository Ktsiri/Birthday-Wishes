using System;
using System.Collections.Generic;
using System.Text;

namespace BirthdayWishes.SmtpClient.Settings
{
    public sealed class EmailSettings
    {
        public int SmtpPort { get; set; }
        public string SmtpHost { get; set; }
        public string NoReplyEmailAddress { get; set; }
        public string ReplyEmailAddress { get; set; }
    }
}
