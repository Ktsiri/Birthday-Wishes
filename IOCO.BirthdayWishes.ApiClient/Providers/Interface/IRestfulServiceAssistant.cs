using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IOCO.BirthdayWishes.ApiClient.Settings.RestSettings;
using IOCO.BirthdayWishes.Dto;

namespace IOCO.BirthdayWishes.ApiClient.Providers.Interface
{
    public interface IRestfulServiceAssistant
    {
        Uri BaseEndpoint { get; set; }

        HttpClient CreateClient();

        HttpClient CreateClient(TokenDto tokenSettings);

        Task<T> GetAsync<T>(string relativePath,
            Dictionary<string, string> headerDictionary = null,
            Dictionary<string, string> queryStringDictionary = null);

        Task<T> PostAsync<T>(Uri requestUrl, T content);

        Task<TResponse> PostAsync<TResponse, TRequest>(string relativePath, TRequest content,
            Dictionary<string, string> headerDictionary = null,
            Dictionary<string, string> queryStringDictionary = null);

        Task<TResponse> PatchAsync<TResponse, TRequest>(string relativePath, TRequest content,
            Dictionary<string, string> headerDictionary = null);

        Task<TokenDto> GetToken(BaseTokenSettings baseTokenSettings,
            string baseTokenUrl, string tokenPath,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
