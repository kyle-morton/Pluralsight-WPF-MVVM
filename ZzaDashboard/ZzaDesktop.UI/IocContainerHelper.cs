using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZzaDashboard.Logic.Services;

namespace ZzaDesktop.UI
{

    public static class IocContainerHelper
    {
        private static IUnityContainer _container;

        /// <summary>
        /// static constructor runs 1x for lifetime of app,
        /// sets up container with types it should inject
        /// </summary>
        static IocContainerHelper()
        {
            _container = new UnityContainer();

            //register types container is responsible for creating
            //when i ask for ICustomersRepository, give me CustomersRepository
            _container.RegisterType<ICustomersRepository, CustomersRepository>(
                new ContainerControlledLifetimeManager()); //make instance provided a singleton
        }

        /// <summary>
        /// singleton instance of IUnityContainer
        /// </summary>
        public static IUnityContainer Container
        {
            get
            {
                return _container;
            }
        }

    }

    
}
