﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BirthdayWishes.Dto
{
    public class ResponseMessageDto
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
