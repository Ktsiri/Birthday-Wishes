using System;
using System.Collections.Generic;
using System.Text;

namespace BirthdayWishes.ApiClient.Settings.RestSettings
{
    public class Credentials
    {
        #region Configuration

        private const string ClientId = "client_id";
        private const string ClientSecret = "client_secret";
        private const string Username = "username";
        private const string Password = "password";
        private const string Scope = "scope";
        private const string GrandType = "grant_type";
        private const string ClientCredentials = "client_credentials";

        #endregion

        public static IEnumerable<KeyValuePair<string, string>> BuildCredentialsDictionary(BaseTokenSettings baseTokenSettings)
        {
            var dict = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(baseTokenSettings.ClientId))
            {
                dict.Add(ClientId, baseTokenSettings.ClientId);
            }

            if (!string.IsNullOrEmpty(baseTokenSettings.ClientSecret))
            {
                dict.Add(ClientSecret, baseTokenSettings.ClientSecret);
            }

            if (!string.IsNullOrEmpty(baseTokenSettings.UserName))
            {
                dict.Add(Username, baseTokenSettings.UserName);
            }

            if (!string.IsNullOrEmpty(baseTokenSettings.UserPassword))
            {
                dict.Add(Password, baseTokenSettings.UserPassword);
            }

            if (!string.IsNullOrEmpty(baseTokenSettings.ScopeSettings))
            {
                dict.Add(Scope, baseTokenSettings.ScopeSettings);
            }

            if (string.IsNullOrWhiteSpace(baseTokenSettings.UserName) || string.IsNullOrWhiteSpace(baseTokenSettings.UserPassword))
            {
                dict.Add(GrandType, ClientCredentials);
            }
            else
            {
                dict.Add(GrandType, Password);
            }

            return dict;
        }
    }
}
