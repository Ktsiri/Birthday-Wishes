using System;
using System.Collections.Generic;
using System.Text;
using IOCO.BirthdayWishes.Contract;
using IOCO.BirthdayWishes.Contract.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace IOCO.BirthdayWishes.Common
{
    public class ConfigureCommonLogicModule : IConfigureDependency
    {
        private readonly IRegisterClass _registerClass;

        public ConfigureCommonLogicModule(IRegisterClass registerClass)
        {
            _registerClass = registerClass;
        }

        public void RegisterDependency()
        {
        }
    }
}
