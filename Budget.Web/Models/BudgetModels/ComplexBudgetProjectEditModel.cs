using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budget.Web.Models.BudgetModels
{
    public class ComplexCompareBudgetEditModel
    {
        public ComplexBudgetProjectEditModel OldBudgetProject { get; set; }

        public ComplexBudgetProjectEditModel NewBudgetProject { get; set; }

        public bool IsApprove { get; set; }
    }

    public class ComplexBudgetProjectEditModel
    {
        public int BaseBudgetId { get; set; }

        public int AdministrativeUnitId { get; set; }

        public string AdministrativeUnitName { get; set; }

        public string Period { get; set; }

        public double TotalIncome { get; set; }

        public double TotalCosts { get; set; }

        public double Balance { get; set; }

        public IList<BudgetCategoryEditModel> Categories { get; set; }
    }

    public class BudgetCategoryEditModel : BaseBudgetItemEditModel
    {
        public IList<TargetBudgetEditModel> Targets { get; set; } 
    }

    public class TargetBudgetEditModel : BaseBudgetItemEditModel
    {
        public IList<BudgetItemEditModel> Items { get; set; }
    }

    public class BudgetItemEditModel : BaseBudgetItemEditModel
    {
    }

    public class BaseBudgetItemEditModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Value { get; set; }

        public int NewValue { get; set; }

        public bool IsEditable { get; set; }

        public int InfoId { get; set; }
    }
}