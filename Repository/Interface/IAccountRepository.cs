using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.ViewModels;

namespace Repository.Interface
{
    public interface IAccountRepository
    {
        Task RegisterNewUser(RegistrationViewModel model, string constr);
        Task<int> LoginUser(LoginViewModel model, string constr);
        Task<int> UserExistsCheck(RegistrationViewModel model, string constr);
    }
}
