using System;
using System.Collections.Generic;
using System.Text;
using Entities.ViewModels;

namespace Repository.Interface
{
    public interface IAccountRepository
    {
        void RegisterNewUser(RegistrationViewModel model, string constr);
    }
}
