using BirthdayWishes.Contract.ApiClient.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BirthdayWishes.ApiClient.Providers.Interface;
using BirthdayWishes.ApiClient.Settings;
using BirthdayWishes.Common.Services;
using BirthdayWishes.Contract.Common;
using BirthdayWishes.Dto;

namespace BirthdayWishes.ApiClient.Requests
{
    [RegisterClassDependency(typeof(IEmployeeExclusionRequest))]
    sealed class EmployeeExclusionRequest : IEmployeeExclusionRequest
    {
        private readonly IRestfulServiceAssistant _restfulServiceAssistant;
        private readonly ISettings<EohMcApiSettings> _settings;

        public EmployeeExclusionRequest(IRestfulServiceAssistant restfulServiceAssistant,
            ISettings<EohMcApiSettings> settings)
        {
            _restfulServiceAssistant = restfulServiceAssistant;
            _settings = settings;
            _restfulServiceAssistant.BaseEndpoint = new Uri(_settings.Value.Url);
        }

        public async Task<bool> IsEmployeeExcludedForCommunication(string employeeId, CancellationToken cancellationToken = default)
        {
            var token = await _restfulServiceAssistant.GetToken(_settings.Value.BaseTokenSettings, _settings.Value.BaseTokenSettings.TokenUrl, _settings.Value.BaseTokenSettings.TokenPath, cancellationToken);

            _restfulServiceAssistant.CreateClient(token);

            var result = await _restfulServiceAssistant.GetAsync<List<EmployeeDto>>(_settings.Value.RelativePathSettings.BirthdayWishExclusions);

            return result.Select(x => x.Id).Contains(employeeId);
        }
    }
}
