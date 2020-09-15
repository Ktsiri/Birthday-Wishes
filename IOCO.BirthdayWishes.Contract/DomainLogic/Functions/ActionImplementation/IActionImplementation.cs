using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IOCO.BirthdayWishes.DomainObjects;
using IOCO.BirthdayWishes.Dto;

namespace IOCO.BirthdayWishes.Contract.DomainLogic.Functions.ActionImplementation
{
    public interface IActionImplementation
    {
        byte ActionId { get; }
        Task<Tuple<ResponseMessageDto, MessageQueue>> PerformAction(MessageQueue messageQueue);
    }
}
