using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BirthdayWishes.DomainObjects;
using BirthdayWishes.Dto;

namespace BirthdayWishes.Contract.DomainLogic.Functions.ActionImplementation
{
    public interface IActionImplementation
    {
        byte MessageStatusId { get; }
        byte? MessageTypeId { get; }
        Task<Tuple<ResponseMessageDto, MessageQueue>> PerformAction(MessageQueue messageQueue);
    }
}
