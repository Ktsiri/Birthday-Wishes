﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BirthdayWishes.Common.Services;
using BirthdayWishes.Contract.DomainLogic.Functions.ActionImplementation;
using BirthdayWishes.DomainObjects;
using BirthdayWishes.Dto;
using BirthdayWishes.Dto.Enumerations;

namespace BirthdayWishes.DomainLogic.Functions.ActionImplementation.QueueActions.New
{
    [RegisterClassDependency(typeof(IActionImplementation))]
    public class AnniversaryMessageTypeAction : IActionImplementation
    {
        public byte MessageStatusId => (byte) MessageStatusEnum.New;

        public byte? MessageTypeId => (byte)MessageTypeEnum.Anniversary;

        public Task<Tuple<ResponseMessageDto, MessageQueue>> PerformAction(MessageQueue messageQueue)
        {
            throw new NotImplementedException();
        }
    }
}
