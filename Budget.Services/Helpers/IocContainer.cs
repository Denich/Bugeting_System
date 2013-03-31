using Budget.Services.BudgetServices;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.BudgetServices.DataProviders;
using Microsoft.Practices.Unity;

namespace Budget.Services.Helpers
{
    public class IocContainer
    {
        private static IocContainer _instance;

        private readonly IUnityContainer _container;

        private IocContainer()
        {
            _container = new UnityContainer().SetDefaults();
        }

        public static IocContainer Instance
        {
            get { return _instance ?? (_instance = new IocContainer()); }
        }

        public void RegisterType<T1, T2>() where T2 : T1
        {
            _container.RegisterType<T1, T2>();
        }

        public void RegisterInstance<TInterface>(string name, TInterface instance)
        {
            _container.RegisterInstance(name, instance);
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}