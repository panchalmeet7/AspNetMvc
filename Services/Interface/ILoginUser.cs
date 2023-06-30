using Entities.Models;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ILoginUser
    {
        Task<User> UserLogin(LoginViewModel model,  string constr);
    }
}
