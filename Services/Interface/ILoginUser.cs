using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interface
{
    public interface ILoginUser
    {
        int UserLogin(LoginViewModel model,  string constr);
    }
}
