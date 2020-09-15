using System;
using System.Collections.Generic;
using System.Text;

namespace IOCO.BirthdayWishes.Contract.DomainLogic.Functions.ActionImplementation
{
    public interface IActionRetriever
    {
        IActionImplementation GetAction(byte actionId);
    }
}
