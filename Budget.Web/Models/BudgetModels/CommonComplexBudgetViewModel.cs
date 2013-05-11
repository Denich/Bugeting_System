using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Budget.Web.Helpers.Converters;
using System.Linq.Expressions;

namespace Budget.Web.Models.BudgetModels
{
    public class CommonComplexBudgetViewModel
    {
        public int AdministrativeUnitId { get; set; }

        public string AdministrativeUnitName { get; set; }

        public int Revision { get; set; }

        public DateTime RevisionDate { get; set; }

        public IEnumerable<BudgetProjectItemViewModel> BudgetItems { get; set; }

        public double TotalIncome { get; set; }

        public double TotalCosts { get; set; }

        public double Balance { get; set; }

        public IEnumerable<CommonComplexBudgetViewModel> FinancialCenterBudgets { get; set; }

        public double GetBudgetItemValue(int budgetItemInfoId)
        {
            var result = BudgetItems.SingleOrDefault(b => b.InfoId == budgetItemInfoId);

            return result == null ? 0 : result.Value;
        }
    }
}