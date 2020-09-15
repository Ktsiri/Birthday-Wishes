using System;
using System.Collections.Generic;
using System.Text;
using IOCO.BirthdayWishes.ApiClient.Settings;
using IOCO.BirthdayWishes.ApiClient.Settings.RestSettings;
using IOCO.BirthdayWishes.Contract;
using IOCO.BirthdayWishes.Contract.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace IOCO.BirthdayWishes.ApiClient
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
