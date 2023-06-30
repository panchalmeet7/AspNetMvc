using Entities.Models;
using Entities.ViewModels;
using Repository.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class LoginUser : ILoginUser
    {
        private readonly IAccountRepository _accountRepository;
        public LoginUser(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<User> UserLogin(LoginViewModel model, string constr)
        {
            return await _accountRepository.LoginUser(model, constr);
            }
    }
}
