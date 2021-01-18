using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BirthdayWishes.Common.Services;
using BirthdayWishes.Contract.DomainLogic.Functions.ActionImplementation;
using BirthdayWishes.DomainObjects;
using BirthdayWishes.Dto;
using BirthdayWishes.Dto.Enumerations;

namespace BirthdayWishes.DomainLogic.Functions.ActionImplementation.MessageType
{
    [RegisterClassDependency(typeof(IActionImplementation))]
    public class EndingWorkMessageTypAction : IActionImplementation
    {
        public byte ActionId => (byte) MessageTypeEnum.EndingWork;
        public Task<Tuple<ResponseMessageDto, MessageQueue>> PerformAction(MessageQueue messageQueue)
        {
            throw new NotImplementedException();
        }
    }
}
