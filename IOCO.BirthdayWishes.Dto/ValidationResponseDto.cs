using System;
using System.Collections.Generic;
using System.Text;

namespace IOCO.BirthdayWishes.Dto
{
    public class ValidationResponseDto
    {
        public List<Error> Errors { get; set; }
        public bool IsValid { get; set; }

    }
    public class Error
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string PropertyName { get; set; }
    }
}
