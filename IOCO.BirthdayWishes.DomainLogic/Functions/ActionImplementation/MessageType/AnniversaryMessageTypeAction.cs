using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IOCO.BirthdayWishes.Common.Services;
using IOCO.BirthdayWishes.Contract.DomainLogic.Functions.ActionImplementation;
using IOCO.BirthdayWishes.DomainObjects;
using IOCO.BirthdayWishes.Dto;
using IOCO.BirthdayWishes.Dto.Enumerations;

namespace IOCO.BirthdayWishes.DomainLogic.Functions.ActionImplementation.MessageType
{
    [RegisterClassDependency(typeof(IActionImplementation))]
    public class AnniversaryMessageTypeAction : IActionImplementation
    {
        public byte ActionId => (byte) MessageTypeEnum.Anniversary;
        public Task<Tuple<ResponseMessageDto, MessageQueue>> PerformAction(MessageQueue messageQueue)
        {
            throw new NotImplementedException();
        }
    }
}
