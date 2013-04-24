using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.BudgetServices.Management;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetServices
{
    public class BudgetClient : IBudgetClient
    {
        public BudgetClient()
        {
        }

        [InjectionConstructor]
        public BudgetClient(IBudgetDataManagement dataManagement, IBudgetOperationManagement operationManagement)
        {
            Data = dataManagement;
            BudgetOperation = operationManagement;
        }

        public IBudgetDataManagement Data { get; set; }

        public IBudgetOperationManagement BudgetOperation { get; set; }
    }
}
