using System;
using System.Collections.Generic;
using System.Text;

namespace BirthdayWishes.Dto
{
    public class TokenDto
    {
        public int ExpiresIn { get; set; }
        public string AccessToken { get; set; }
    }
}
