using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IOCO.BirthdayWishes.Dto;

namespace IOCO.BirthdayWishes.Contract.ApiClient.Requests
{
    public interface ICelebrationRequest
    {
        Task<List<EmployeeDto>> GetEmployeeBirthdayForToday(DateTime date, CancellationToken cancellationToken = default);
        Task<List<EmployeeDto>> GetEmployeeStartingWorkToday(DateTime date, CancellationToken cancellationToken = default);
    }
}
