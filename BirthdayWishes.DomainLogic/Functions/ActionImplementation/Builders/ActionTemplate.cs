using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirthdayWishes.Common.Services;
using BirthdayWishes.Contract.DomainLogic.Functions.ActionImplementation;
using BirthdayWishes.DomainObjects;
using BirthdayWishes.Dto;

namespace BirthdayWishes.DomainLogic.Functions.ActionImplementation.Builders
{
    [RegisterClassDependency(typeof(IActionTemplate))]
    sealed class ActionTemplate : IActionTemplate
    {
        public async Task<Tuple<ResponseMessageDto, MessageQueue>> ExecuteActionTemplate(Func<MessageQueue, Task<Tuple<ResponseMessageDto, MessageQueue>>> action,
            MessageQueue messageQueue)
        {
            if (messageQueue == null)
            {
                throw new ArgumentNullException(nameof(messageQueue));
            }

            var responseMessageDto = new ResponseMessageDto { Success = true, Errors = new List<string>() };
            var response = new Tuple<ResponseMessageDto, MessageQueue>(responseMessageDto, messageQueue);

            try
            {
                if (action == null)
                {
                    throw new ArgumentNullException(nameof(action));
                }
                response = await action(messageQueue);
            }
            catch (Exception ex)
            {
                response.Item1.Success = false;
                response.Item1.Errors = new List<string> { ex.Message };
                // TODO: log exception 
            }

            return response;
        }
    }
}
