using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Entities.ViewModels;

namespace Repository.Interface
{
    public interface IAccountRepository
    {
        Task RegisterNewUser(RegistrationViewModel model, string constr);
        Task<User> LoginUser(LoginViewModel model, string constr);
        Task<int> UserExistsCheck(RegistrationViewModel model, string constr);
    }
}
