using System;
using System.Collections.Generic;
using System.Text;
using Entities.ViewModels;

namespace Services.Interface
{
    public interface IRegisterUser
    {
        void Register(RegistrationViewModel model, string constr);
    }
}
