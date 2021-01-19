using System;
using System.Collections.Generic;
using System.Text;

namespace BirthdayWishes.Contract.DomainLogic.Functions.ActionImplementation
{
    public interface IActionRetriever
    {
        IActionImplementation GetAction(byte messageStatusId, byte? messageTypeId);
    }
}
