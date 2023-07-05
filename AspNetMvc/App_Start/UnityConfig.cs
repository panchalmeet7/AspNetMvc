using Repository.Interface;
using Repository.Repository;
using Services.Interface;
using Services.Service;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace AspNetMvc
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
             container.RegisterType<IRegisterUser, RegisterUser>();
             container.RegisterType<ILoginUser, LoginUser>();
             container.RegisterType<IGetEmployee, GetEmployee>();
             container.RegisterType<IAccountRepository, AccountRepository>();


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}