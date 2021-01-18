using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BirthdayWishes.Common.Services;
using BirthdayWishes.Contract.DomainLogic.Functions.ActionImplementation;
using BirthdayWishes.Contract.DomainLogic.Retreivers;
using BirthdayWishes.DomainObjects;
using BirthdayWishes.Dto;
using BirthdayWishes.Dto.Enumerations;

namespace BirthdayWishes.DomainLogic.Functions.ActionImplementation.MessageStatus
{
    [RegisterClassDependency(typeof(IActionImplementation))]
    sealed class NewMessageAction : IActionImplementation
    {
        private readonly IActionTemplate _actionTemplate;
        private readonly IActionRetriever _actionRetriever;
        public byte ActionId => (byte)MessageStatusEnum.New;
        public NewMessageAction(IActionTemplate actionTemplate, IActionRetriever actionRetriever)
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
            var action = _actionRetriever.GetAction((byte)messageQueue.MessageType);
            var actionResults = await action.PerformAction(messageQueue);

            messageQueue.MessageStatus = actionResults.Item1.Success ? MessageStatusEnum.Sent : MessageStatusEnum.Failed;

            return actionResults;
        }
    }
}
