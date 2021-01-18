using System;
using System.Collections.Generic;
using System.Text;

namespace BirthdayWishes.Dto
{
    public sealed class EmailDto
    {
        public List<string> ToList { get; set; }
        public List<KeyValuePair<string, string>> HeadersList { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
        public bool ReplyExpected { get; set; }
    }
}
