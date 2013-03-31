using Budget.Services.Helpers;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetServices
{
    public class BudgetServiceFactory : IBudgetServiceFactory
    {
        public IBudgetClient GetBudgetClient(string connectionString)
        {
            var container =  IocContainer.Instance;
            
            container.RegisterInstance("ConnectionString", connectionString);
            
            return container.Resolve<IBudgetClient>();
        }
    }
}
