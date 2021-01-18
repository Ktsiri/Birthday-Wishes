using System;
using System.Collections.Generic;
using System.Text;

namespace BirthdayWishes.ApiClient.Settings.RestSettings
{
    public class BaseTokenSettings
    {
        public bool TokenNeeded { get; set; }
        public string TokenUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string TokenPath { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string ScopeSettings { get; set; }
        public string ResultPath { get; set; }
    }
}
