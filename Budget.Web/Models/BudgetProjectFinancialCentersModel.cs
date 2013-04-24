using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budget.Web.Models
{
    public class BudgetProjectFinancialCentersModel
    {
        public BudgetProjectFinancialCentersModel(int year, IEnumerable<FinancialCenterSelectModel> financialCenters)
        {
            Year = year;
            FinancialCenters = financialCenters.ToList();
        }

        public BudgetProjectFinancialCentersModel()
        {
        }

        public int Year { get; set; }

        public List<FinancialCenterSelectModel> FinancialCenters { get; set; }
    }
}