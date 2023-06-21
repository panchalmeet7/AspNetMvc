using Repository.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.ViewModels;
using System.Threading.Tasks;

namespace Services.Service
{
    public class RegisterUser : IRegisterUser
    {
        private readonly IAccountRepository _accountRepository;
        public RegisterUser(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task Register(RegistrationViewModel model, string constr)
        {
             await _accountRepository.RegisterNewUser(model, constr);
        }

        public async Task<int> EmailCheck(RegistrationViewModel model, string constr)
        {
            return await _accountRepository.UserExistsCheck(model, constr);
        }

    }
}
