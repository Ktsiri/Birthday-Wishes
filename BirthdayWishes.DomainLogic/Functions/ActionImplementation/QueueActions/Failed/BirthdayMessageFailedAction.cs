using BirthdayWishes.Common.Services;
using BirthdayWishes.Contract.DomainLogic.Functions.ActionImplementation;
using BirthdayWishes.DomainObjects;
using BirthdayWishes.Dto;
using BirthdayWishes.Dto.Enumerations;
using System;
using System.Threading.Tasks;

namespace BirthdayWishes.DomainLogic.Functions.ActionImplementation.QueueActions.Failed
{
    [RegisterClassDependency(typeof(IActionImplementation))]
    sealed class BirthdayMessageFailedAction : IActionImplementation
    {
        private readonly IActionTemplate _actionTemplate;
        private readonly IActionRetriever _actionRetriever;

        public byte MessageStatusId => (byte)MessageStatusEnum.Failed;

        public byte? MessageTypeId => (byte)MessageTypeEnum.Birthday;

        public BirthdayMessageFailedAction(IActionTemplate actionTemplate, IActionRetriever actionRetriever)
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
            var action = _actionRetriever.GetAction((byte)MessageStatusEnum.New, (byte)messageQueue.MessageType);
            var actionResults = await action.PerformAction(messageQueue);

            messageQueue.MessageStatus = actionResults.Item1.Success ? MessageStatusEnum.Sent : MessageStatusEnum.Failed;

            return actionResults;
        }
    }
}
