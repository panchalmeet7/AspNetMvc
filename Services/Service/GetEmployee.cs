using Entities.Models;
using Repository.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class GetEmployee : IGetEmployee
    {
        public readonly IAccountRepository _accountRepository;
        public GetEmployee(IAccountRepository  accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public Task GetEmployeeData(string connectionStr)
        {
            return _accountRepository.GetAllEmployeeData(connectionStr);
        }
    }
}
