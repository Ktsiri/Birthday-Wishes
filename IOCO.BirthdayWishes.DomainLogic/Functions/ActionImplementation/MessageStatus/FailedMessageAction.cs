using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IOCO.BirthdayWishes.Common.Services;
using IOCO.BirthdayWishes.Contract.DomainLogic.Functions.ActionImplementation;
using IOCO.BirthdayWishes.Contract.DomainLogic.Retreivers;
using IOCO.BirthdayWishes.DomainObjects;
using IOCO.BirthdayWishes.Dto;
using IOCO.BirthdayWishes.Dto.Enumerations;

namespace IOCO.BirthdayWishes.DomainLogic.Functions.ActionImplementation.MessageStatus
{
    [RegisterClassDependency(typeof(IActionImplementation))]
    sealed class FailedMessageAction : IActionImplementation
    {
        private readonly IActionTemplate _actionTemplate;
        private readonly IActionRetriever _actionRetriever;
        public byte ActionId => (byte)MessageStatusEnum.Failed;
        public FailedMessageAction(IActionTemplate actionTemplate, IActionRetriever actionRetriever)
        {
            _actionTemplate = actionTemplate;
            _actionRetriever = actionRetriever;
        }
        public async Task<Tuple<ResponseMessageDto, MessageQueue>> PerformAction(MessageQueue messageQueue)
        {
            return await _actionTemplate.ExecuteActionTemplate(Action, messageQueue);
        }

        private async Task<Tuple<ResponseMessageDto, MessageQueue>> Action(MessageQueue messageQueue)
        {
            var action = _actionRetriever.GetAction((byte)MessageStatusEnum.New);
            var actionResults = await action.PerformAction(messageQueue);

            return actionResults;
        }
    }
}
