using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BirthdayWishes.Common.Services;
using BirthdayWishes.Contract.DomainLogic.Functions.ActionImplementation;
using BirthdayWishes.DomainObjects;
using BirthdayWishes.Dto;
using BirthdayWishes.Dto.Enumerations;

namespace BirthdayWishes.DomainLogic.Functions.ActionImplementation.MessageStatus
{
    [RegisterClassDependency(typeof(IActionImplementation))]
    sealed class SentMessageAction : IActionImplementation
    {
        public byte ActionId => (byte) MessageStatusEnum.Sent;
        public Task<Tuple<ResponseMessageDto, MessageQueue>> PerformAction(MessageQueue messageQueue)
        {
            throw new NotImplementedException();
        }
    }
}
