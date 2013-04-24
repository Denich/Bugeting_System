using System.Collections.Generic;
using Budget.Web.Models.BudgetModels;

namespace Budget.Web.Models
{
    public class ActiveBudgetsListViewModel
    {
        public YearComplexBudgetListViewModel CurrentYearBudget { get; set; }

        public QuarterComplexBudgetListViewModel CurrentQuarterBudget { get; set; }

        public MonthComplexBudgetListViewModel CurrentMonthBudget { get; set; }

        public IEnumerable<YearComplexBudgetResultListViewModel> GetResultsYearBudgets { get; set; }

        public IEnumerable<QuarterComplexBudgetResultListViewModel> GetResultsQuarterBudgets { get; set; }

        public IEnumerable<MonthComplexBudgetResultListViewModel> GetResultsMonthBudgets { get; set; }

        public IEnumerable<YearUnapproveComplexBudgetInfoModel> ApprovalProccesYearBudgets { get; set; }

        public IEnumerable<QuarterUnapproveComplexBudgetInfoModel> ApprovalProccesQuarterBudgets { get; set; }

        public IEnumerable<MonthUnapproveComplexBudgetInfoViewModel> ApprovalProccesMonthBudgets { get; set; }
    }
}