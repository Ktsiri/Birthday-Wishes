﻿using System;
using System.Collections.Generic;
using System.Text;
using BirthdayWishes.ApiClient.Settings.RestSettings;

namespace BirthdayWishes.ApiClient.Settings
{
    public class EohMcApiSettings 
    {
        public string Url { get; set; }
        public BaseTokenSettings BaseTokenSettings { get; set; }
        public RelativePathSettings RelativePathSettings { get; set; }
    }

    public class RelativePathSettings
    {
        public string BirthdayWishExclusions { get; set; }
        public string Employees { get; set; }
    }
}
