using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.ViewModels;

namespace Services.Interface
{
    public interface IRegisterUser
    {
        Task Register(RegistrationViewModel model, string constr);
        Task<int> EmailCheck(RegistrationViewModel model, string constr);
    }
}
