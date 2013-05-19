using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budget.Web.Models
{
    public class BudgetProjectFinancialCentersModel
    {
        public BudgetProjectFinancialCentersModel(int year, int companyBaseYearBudgetId, IEnumerable<FinancialCenterSelectModel> financialCenters)
        {
            Year = year;
            FinancialCenters = financialCenters.ToList();
            CompanyBaseYearBudgetId = companyBaseYearBudgetId;
        }

        public BudgetProjectFinancialCentersModel()
        {
        }

        public int Year { get; set; }

        public int CompanyBaseYearBudgetId { get; set; }

        public List<FinancialCenterSelectModel> FinancialCenters { get; set; }

        public bool IsNewBudget { get; set; }
    }
}