using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IOCO.BirthdayWishes.Common.Helpers;
using IOCO.BirthdayWishes.Common.Services;
using IOCO.BirthdayWishes.Contract.DomainLogic.Functions.ActionImplementation;

namespace IOCO.BirthdayWishes.DomainLogic.Functions.ActionImplementation.Builders
{
    [RegisterClassDependency(typeof(IActionRetriever))]
    sealed class ActionRetriever : IActionRetriever
    {
        private readonly IServiceProvider _serviceProvider;

        public ActionRetriever(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public IActionImplementation GetAction(byte actionId)
        {
            var types = ReflectionHelper.GetTypes<IActionImplementation>();

            return (from type in types select type.GetConstructors().Single() 
                into constructor let parameters = constructor.GetParameters()
                    .Select(x => _serviceProvider.GetService(x.ParameterType))
                    .ToArray() select constructor.Invoke(parameters) 
                into method where ((IActionImplementation) method).ActionId == actionId 
                select method).Cast<IActionImplementation>().FirstOrDefault();
        }
    }
}
