using System;
using System.Collections.Generic;
using System.Text;
using BirthdayWishes.Contract;
using BirthdayWishes.Contract.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace BirthdayWishes.Common
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
