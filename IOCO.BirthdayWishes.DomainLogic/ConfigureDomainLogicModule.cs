using System;
using System.Collections.Generic;
using System.Text;
using IOCO.BirthdayWishes.Contract;
using IOCO.BirthdayWishes.Contract.DependencyInjection;
using IOCO.BirthdayWishes.Contract.DomainLogic.Functions.ActionImplementation;
using IOCO.BirthdayWishes.DomainLogic.Functions.ActionImplementation.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace IOCO.BirthdayWishes.DomainLogic
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
