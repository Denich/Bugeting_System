using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Budget.Services.BudgetServices.DataProviderContracts;

namespace Budget.Services.BudgetServices
{
    public class BudgetClient : IBudgetClient
    {
        public BudgetClient()
        {
        }

        public BudgetClient(IBudgetDataManagement dataManagement)
        {
            DataManagement = dataManagement;
        }

        public IBudgetDataManagement DataManagement { get; set; }
    }
}
