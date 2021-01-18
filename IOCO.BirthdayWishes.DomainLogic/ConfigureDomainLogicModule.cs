using System;
using System.Collections.Generic;
using System.Text;
using BirthdayWishes.Contract;
using BirthdayWishes.Contract.DependencyInjection;
using BirthdayWishes.Contract.DomainLogic.Functions.ActionImplementation;
using BirthdayWishes.DomainLogic.Functions.ActionImplementation.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace BirthdayWishes.DomainLogic
{
    public class ConfigureDomainLogicModule : IConfigureDependency
    {
        private readonly IRegisterClass _registerClass;

        public ConfigureDomainLogicModule(IRegisterClass registerClass)
        {
            _registerClass = registerClass;
        }

        public void RegisterDependency()
        {
            //_registerClass.RegisterClass(typeof(IActionRetriever), typeof(ActionRetriever), ServiceLifetime.Scoped);
        }
    }
}
