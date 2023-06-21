using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ILoginUser
    {
        Task<int> UserLogin(LoginViewModel model,  string constr);
    }
}
