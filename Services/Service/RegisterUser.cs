using Repository.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.ViewModels;

namespace Services.Service
{
    public class RegisterUser : IRegisterUser
    {
        private readonly IAccountRepository _accountRepository;
        public RegisterUser(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void Register(RegistrationViewModel model, string constr)
        {
            _accountRepository.RegisterNewUser(model, constr);

        }

        public int EmailCheck(RegistrationViewModel model, string constr)
        {
            return _accountRepository.UserExistsCheck(model, constr);
        }

    }
}
