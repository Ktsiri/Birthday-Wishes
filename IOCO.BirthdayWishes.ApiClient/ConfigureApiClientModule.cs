using System;
using System.Collections.Generic;
using System.Text;
using BirthdayWishes.ApiClient.Settings;
using BirthdayWishes.ApiClient.Settings.RestSettings;
using BirthdayWishes.Contract;
using BirthdayWishes.Contract.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace BirthdayWishes.ApiClient
{
    public class ConfigureApiClientModule : IConfigureDependency
    {
        private readonly IRegisterClass _registerClass;

        public ConfigureApiClientModule(IRegisterClass registerClass)
        {
            _registerClass = registerClass;
        }

        public void RegisterDependency()
        {
            _registerClass.ConfigureSettings<BaseTokenSettings>("BaseTokenSettings")
            .ConfigureSettings<EohMcApiSettings>("EohMcApiSettings")
            .ConfigureSettings<RelativePathSettings>("RelativePathSettings")
            .Services.AddHttpClient();
        }
    }
}
