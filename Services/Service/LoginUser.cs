using Entities.ViewModels;
using Repository.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Service
{
    public class LoginUser : ILoginUser
    {
        private readonly IAccountRepository _accountRepository;
        public LoginUser(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public int UserLogin(LoginViewModel model, string constr)
        {
            return _accountRepository.LoginUser(model, constr);
        }
    }
}
