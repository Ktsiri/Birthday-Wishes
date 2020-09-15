using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using IOCO.BirthdayWishes.ApiClient.Providers.Interface;
using IOCO.BirthdayWishes.ApiClient.Settings.RestSettings;
using IOCO.BirthdayWishes.Common.Converters;
using IOCO.BirthdayWishes.Common.Services;
using IOCO.BirthdayWishes.Dto;

namespace IOCO.BirthdayWishes.ApiClient.Providers.Implementation
{
    [RegisterClassDependency(typeof(IRestfulServiceAssistant))]
    sealed class RestfulServiceAssistant : IRestfulServiceAssistant
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient _httpClient;
        public Uri BaseEndpoint { get; set; }
        public RestfulServiceAssistant(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException("httpClientFactory");
            _httpClient = CreateClient();
        }

        public HttpClient CreateClient()
        {
            if (_httpClient == null)
            {
                _httpClient = _httpClientFactory.CreateClient();
            }

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return _httpClient;
        }

        public HttpClient CreateClient(TokenDto tokenSettings)
        {
            if (_httpClient == null)
            {
                _httpClient = _httpClientFactory.CreateClient();
            }

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (tokenSettings != null)
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenSettings.AccessToken);

            return _httpClient;
        }
        /// <summary>  
        /// Common method for making GET calls  
        /// </summary>  
        public async Task<T> GetAsync<T>(string relativePath,
            Dictionary<string, string> headerDictionary = null,
            Dictionary<string, string> queryStringDictionary = null)
        {
            T result = default(T);
            string queryString = string.Empty;

            if (queryStringDictionary != null)
                queryString = BuildQueryString(queryStringDictionary);

            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                relativePath), queryString);

            if (headerDictionary != null)
            {
                foreach (var headerItem in headerDictionary)
                {
                    _httpClient.DefaultRequestHeaders.Add(headerItem.Key, headerItem.Value);
                }
            }

            var response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(data))
                result = JsonConverter.ConvertFromJson<T>(data);
            return result;
        }

        /// <summary>  
        /// Common method for making POST calls  
        /// </summary>  
        public async Task<T> PostAsync<T>(Uri requestUrl, T content)
        {
            var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T>(content));
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConverter.ConvertFromJson<T>(data);
            }

            return default(T);
        }

        public async Task<TResponse> PostAsync<TResponse, TRequest>(string relativePath, TRequest content,
            Dictionary<string, string> headerDictionary = null,
            Dictionary<string, string> queryStringDictionary = null)
        {
            string queryString = string.Empty;

            if (queryStringDictionary != null)
                queryString = BuildQueryString(queryStringDictionary);

            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                relativePath), queryString);

            if (headerDictionary != null)
            {
                foreach (var headerItem in headerDictionary)
                {
                    _httpClient.DefaultRequestHeaders.Add(headerItem.Key, headerItem.Value);
                }
            }
            var httpContent = CreateHttpContent(content);
            var response = await _httpClient.PostAsync(requestUrl.ToString(), httpContent);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConverter.ConvertFromJson<TResponse>(data);
            }

            return default(TResponse);
        }

        public async Task<TResponse> PatchAsync<TResponse, TRequest>(string relativePath, TRequest content,
            Dictionary<string, string> headerDictionary = null)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                relativePath), string.Empty);

            if (headerDictionary != null)
            {
                foreach (var headerItem in headerDictionary)
                {
                    _httpClient.DefaultRequestHeaders.Add(headerItem.Key, headerItem.Value);
                }
            }

            var response = await _httpClient.PatchAsync(requestUrl.ToString(), CreateHttpContent(content));
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConverter.ConvertFromJson<TResponse>(data);
            }

            return default(TResponse);
        }
        public async Task<TokenDto> GetToken(BaseTokenSettings baseTokenSettings,
            string baseTokenUrl, string tokenPath,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (!baseTokenSettings.TokenNeeded)
                return null;

            var clientCredentials = Credentials.BuildCredentialsDictionary(baseTokenSettings);

            var builder = new UriBuilder(new Uri(baseTokenUrl)) { Path = tokenPath };

            var response = await _httpClient.PostAsync(builder.Uri, new FormUrlEncodedContent(clientCredentials), cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConverter.ConvertFromJson<TokenDto>(data);
            }

            return null;
        }

        private Uri CreateRequestUri(string relativePath, string queryString)
        {
            var endpoint = new Uri(BaseEndpoint, relativePath);
            var uriBuilder = new UriBuilder(endpoint) { Query = queryString };

            return uriBuilder.Uri;
        }

        private HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConverter.ConvertToJson(content);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
        private string BuildQueryString(Dictionary<string, string> dictionary)
        {
            var collection = HttpUtility.ParseQueryString(string.Empty);
            foreach (var item in dictionary.Where(i => !string.IsNullOrEmpty(i.Value) && !string.IsNullOrEmpty(i.Key)))
            {
                collection[item.Key] = item.Value;
            }

            var query = collection.ToString();

            return query;
        }
    }
}
