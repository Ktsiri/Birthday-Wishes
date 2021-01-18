using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BirthdayWishes.Contract.ApiClient.Requests
{
    public interface IEmployeeExclusionRequest
    {
        Task<bool> IsEmployeeExcludedForCommunication(string employeeId, CancellationToken cancellationToken = default);
    }
}
